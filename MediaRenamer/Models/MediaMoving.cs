using MediaRenamer.TMDb.Models.Movies;

namespace MediaRenamer.Models;

public class MediaMoving
{
    public DirectoryInfo DestinationDirectory { get; }
    public string FileName { get; }
    public IEnumerable<string> ExtraPaths { get; private set; }

    public MediaMoving(DirectoryInfo destinationDirectory, string fileName)
    {
        DestinationDirectory = destinationDirectory;
        ExtraPaths = Enumerable.Empty<string>();
        FileName = fileName;
    }


    #region methods
    public MediaMoving WithExtraPaths(params string?[] paths)
    {
        ExtraPaths = paths.Where(path => !string.IsNullOrWhiteSpace(path)).Cast<string>();
        return this;
    }

    public string GetBasePath()
    {
        var path = Path.Combine(ExtraPaths.ToArray());
        return Path.Combine(DestinationDirectory.FullName, path);
    }

    public FileInfo GetFile(string exension)
        => new (Path.Combine(GetBasePath(), FileName + exension));
    #endregion
}
