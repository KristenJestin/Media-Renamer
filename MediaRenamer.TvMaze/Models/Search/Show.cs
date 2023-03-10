
using MediaRenamer.TvMaze.Models.General;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRenamer.TvMaze.Models.Search
{

    public class ShowRoot
    {
        [JsonProperty("score")]
        public double? Score { get; set; }

        [JsonProperty("show")]
        public Show Show { get; set; }
    }

    public class Show
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("genres")]
        public List<string> Genres { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("runtime")]
        public int? Runtime { get; set; }

        [JsonProperty("averageRuntime")]
        public int? AverageRuntime { get; set; }

        [JsonProperty("premiered")]
        public string Premiered { get; set; }

        [JsonProperty("ended")]
        public object Ended { get; set; }

        [JsonProperty("officialSite")]
        public string OfficialSite { get; set; }

        [JsonProperty("schedule")]
        public Schedule Schedule { get; set; }

        [JsonProperty("rating")]
        public Rating Rating { get; set; }

        [JsonProperty("weight")]
        public int? Weight { get; set; }

        [JsonProperty("network")]
        public Network Network { get; set; }

        [JsonProperty("webChannel")]
        public WebChannel WebChannel { get; set; }

        [JsonProperty("dvdCountry")]
        public object DvdCountry { get; set; }

        [JsonProperty("externals")]
        public Externals Externals { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("updated")]
        public int? Updated { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }
    }       
}
