using MediaRenamer.TMDb.Models.Movies;

namespace MediaRenamer.Models;

public class MediaMoving
{
    public DirectoryInfo DestinationDirectory { get; }
    public string FileName { get; }
    public IEnumerable<string> ExtraPaths { get; }

    public MediaMoving(DirectoryInfo destinationDirectory, string fileName, string[]? extraPaths = null)
    {
        DestinationDirectory = destinationDirectory;
        ExtraPaths = extraPaths ?? Enumerable.Empty<string>();
        FileName = fileName;
    }


    #region methods
    public string GetBasePath()
    {
        var path = Path.Combine(ExtraPaths.ToArray());
        return Path.Combine(DestinationDirectory.FullName, path);
    }

    public FileInfo GetFile(string exension)
        => new (Path.Combine(GetBasePath(), FileName + exension));
    #endregion
}
