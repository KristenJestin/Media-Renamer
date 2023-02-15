using System.IO;

namespace MediaRenamer.Media.Models
{
    public class MediaData
    {
        public FileInfo File { get; }
        public MediaParserResult Data { get; }

        public MediaData(FileInfo file)
        {
            File = file;
            var name = file.Name;
            Data = MediaParser.Default.Parse(name);
        }

        public static MediaData Create(FileInfo file)
            => new MediaData(file);
    }
}