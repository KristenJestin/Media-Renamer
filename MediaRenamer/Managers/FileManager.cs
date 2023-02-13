using MediaRenamer.Models;
using MediaRenamer.Models.Medias;

namespace MediaRenamer.Managers;

public static class FileManager
{
    private static readonly IEnumerable<string> _masks = new List<string> { "mkv", "ogv", "avi", "wmv", "asf", "mp4", "m4p", "m4v", "mpeg", "mpg", "mpe", "mpv", "mpg", "m2v" };

    public static IEnumerable<MediaData> GetFilesFromSource(this AppConfig config)
    {
        var files = config.Directory
            .EnumerateFiles("*.*", config.OnlyTopDirectory ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories)
            .Where(file => _masks.Contains(file.Extension[1..]));

        return files.Select(MediaData.Create);
    }
}
