using System.IO;

namespace MediaRenamer.Media.Models
{
    public class MediaFile
    {
        public FileInfo File { get; }
        public MediaParserResult ExtractedData { get; }
        public MediaData? Data { get; private set; }

        public MediaFile(FileInfo file)
        {
            File = file;
            var name = file.Name;
            ExtractedData = MediaParser.Default.Parse(name);
        }


        #region methods
        public void SetData(MediaData? data)
            => Data = data;
        #endregion

        #region statics
        public static MediaFile Create(FileInfo file)
            => new MediaFile(file);
        #endregion
    }
}