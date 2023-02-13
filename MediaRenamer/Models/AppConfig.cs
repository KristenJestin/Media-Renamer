namespace MediaRenamer.Models;

public class AppConfig
{
    public string SourcePath { get; set; }
    public bool OnlyTopDirectory { get; set; }


    private DirectoryInfo? directory = null;
    public DirectoryInfo Directory => directory ??= new DirectoryInfo(SourcePath);
}

