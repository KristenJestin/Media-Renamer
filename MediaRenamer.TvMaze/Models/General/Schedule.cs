using Newtonsoft.Json;
using System.Collections.Generic;

namespace MediaRenamer.TvMaze.Models.General
{
    public class Schedule
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("days")]
        public List<string> Days { get; set; }
    }
}
