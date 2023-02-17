using Newtonsoft.Json;

namespace MediaRenamer.TvMaze.Models.General
{
    public class Links
    {
        [JsonProperty("self")]
        public HrefObj Self { get; set; }

        [JsonProperty("previousepisode")]
        public HrefObj PreviousEpisode { get; set; }

        [JsonProperty("nextepisode")]
        public HrefObj NextEpisode { get; set; }
    }
}
