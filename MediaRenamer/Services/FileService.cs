using MediaRenamer.Common.Exceptions;
using MediaRenamer.Common.Extensions;
using MediaRenamer.Media.Models;
using MediaRenamer.Models;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace MediaRenamer.Services;

public class FileService
{
    private readonly AppConfig _config;
    private readonly Dictionary<MediaType, Func<MediaFile, MediaMoving>> _converters = new();

    public FileService(IOptions<AppConfig> config)
    {
        _config = config.Value;

        _converters.Add(MediaType.Movie, RenameMovie);
        _converters.Add(MediaType.Tv, RenameTv);
    }

    public IEnumerable<MediaFile> GetMediaFilesFromSource(DirectoryInfo directory)
    {
        var files = directory
            .EnumerateFiles("*.*", _config.OnlyTopDirectory ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories)
            .Where(file => _config.GetMask().Contains(file.Extension));

        return files.Select(MediaFile.Create);
    }

    public MediaMoving BuildMovingMedia(MediaFile media)
    {
        var type = media.ExtractedData.Type;
        if (!_converters.ContainsKey(type))
            throw new Exception($"The type of media provided ({type}) is not supported");

        return _converters[type](media);
    }

    public async Task CopyFileAsync(FileInfo source, MediaMoving moving, Action<double, CancelEventArgs>? onProgressChanged = null, bool overwrite = false)
    {
        if (source.DirectoryName == null)
            throw new ArgumentNullException(nameof(source));

        var destination = moving.GetFile(source.GetExtension());

        // check if already exists
        if (!overwrite && destination.Exists)
            throw new FileExistsException($"The destination already exists (enable {overwrite} to replace it).", moving.FileName);

        // create folder if not exists
        destination.Directory?.Create();

        var buffer = new byte[1024 * 1024]; // 1MB buffer
        var cancel = new CancelEventArgs();

        using var sourceStream = new FileStream(source.FullName, FileMode.Open, FileAccess.Read);
        var fileLength = sourceStream.Length;
        using var destinationStream = new FileStream(destination.FullName, overwrite ? FileMode.Create : FileMode.CreateNew, FileAccess.Write);
        long totalBytes = 0;
        var currentBlockSize = 0;

        // TODO: use cancelation token on Task
        while ((currentBlockSize = await sourceStream.ReadAsync(buffer)) > 0)
        {
            totalBytes += currentBlockSize;
            var percentage = totalBytes * 100.0 / fileLength;

            // TODO: use cancelation token on Task
            await destinationStream.WriteAsync(buffer.AsMemory(0, currentBlockSize));

            onProgressChanged?.Invoke(percentage, cancel);

            if (cancel.Cancel)
            {
                destination.Delete();
                break;
            }
        }
    }


    #region privates
    private MediaMoving RenameMovie(MediaFile media)
    {
        var fileName = BetterNamingAndApplyReplacements(media.Data!.Title);
        if (media.Data.ReleaseDate.HasValue)
            fileName += $" ({media.Data.ReleaseDate:yyyy})";

        return new MediaMoving(_config.MovieDestination, fileName);
    }

    private MediaMoving RenameTv(MediaFile media)
    {
        var tv = BetterNamingAndApplyReplacements(media.Data!.Title);
        var tvDate = media.Data.ReleaseDate.HasValue ? $" ({media.Data.ReleaseDate:yyyy})" : string.Empty;
        var season = $"Season {media.Data.Season:d2}";
        var episode = BetterNamingAndApplyReplacements(media.Data.EpisodeTitle!);
        var fileName = $"{tv} - S{media.Data.Season:d2}E{media.Data.Episode:d2} - {episode}";

        return new MediaMoving(_config.TvDestination, fileName, new[] { tv + tvDate, season });
    }

    private string BetterNamingAndApplyReplacements(string fileName)
    {
        var result = fileName;

        // replace by user (or default config) data
        foreach (var replacement in _config.Replacements)
            result = result.Replace(replacement.Key, replacement.Value);

        // remove old invalid char if any are left
        foreach (var character in Path.GetInvalidFileNameChars())
            result = result.Replace(character, ' ');

        // remove double space
        result = Regex.Replace(result, @"\s+", " ");

        return result;
    }
    #endregion
}
