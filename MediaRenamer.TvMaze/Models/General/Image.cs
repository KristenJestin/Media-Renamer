using Newtonsoft.Json;

namespace MediaRenamer.TvMaze.Models.General
{
    public class Image
    {
        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("original")]
        public string Original { get; set; }
    }
}
