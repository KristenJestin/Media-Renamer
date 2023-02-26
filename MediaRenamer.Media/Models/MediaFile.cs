using System.Collections.Generic;
using System.IO;

namespace MediaRenamer.Media.Models
{
    public class MediaFile
    {
        public FileInfo File { get; }
        public MediaParserResult ExtractedData { get; }
        public MediaData? Data { get; private set; }

        public MediaFile(FileInfo file, Dictionary<string, string> replacements)
        {
            File = file;

            var name = file.Name;
            // replace by user (or default config) data
            foreach (var replacement in replacements)
                name = name.Replace(replacement.Key, replacement.Value);

            ExtractedData = MediaParser.Default.Parse(name);
        }


        #region methods
        public void SetData(MediaData? data)
            => Data = data;
        #endregion
    }
}