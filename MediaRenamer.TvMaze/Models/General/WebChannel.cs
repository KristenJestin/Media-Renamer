using Newtonsoft.Json;

namespace MediaRenamer.TvMaze.Models.General
{
    public class WebChannel
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public object Country { get; set; }

        [JsonProperty("officialSite")]
        public string OfficialSite { get; set; }
    }
}
