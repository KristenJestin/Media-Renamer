using Newtonsoft.Json;

namespace MediaRenamer.TvMaze.Models.General
{
    public class Network
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("officialSite")]
        public object OfficialSite { get; set; }
    }
}
