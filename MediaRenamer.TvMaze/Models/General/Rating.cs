using Newtonsoft.Json;

namespace MediaRenamer.TvMaze.Models.General
{
    public class Rating
    {
        [JsonProperty("average")]
        public double? Average { get; set; }
    }
}
