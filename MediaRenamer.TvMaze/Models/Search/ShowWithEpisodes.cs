using MediaRenamer.TvMaze.Models.Shows;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MediaRenamer.TvMaze.Models.Search
{
    public class ShowWithEpisodes : Show
    {
        [JsonProperty("_embedded")]
        public ShowWithEpisodesEmbeded Embededs { get; set; }
    }

    public class ShowWithEpisodesEmbeded
    {
        [JsonProperty("episodes")]
        public List<Episode> Episodes { get; set; }
    }
}
