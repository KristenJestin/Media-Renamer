using Newtonsoft.Json;

namespace MediaRenamer.TvMaze.Models.General
{
    public class HrefObj
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
