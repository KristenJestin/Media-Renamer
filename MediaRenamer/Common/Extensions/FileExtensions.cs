namespace MediaRenamer.Common.Extensions;

public static class FileExtensions
{
    public static string GetExtension(this FileInfo file)
        => Path.GetExtension(file.FullName);
    public static string NameWithoutExtension(this FileInfo file)
        => Path.GetFileNameWithoutExtension(file.FullName);
}
