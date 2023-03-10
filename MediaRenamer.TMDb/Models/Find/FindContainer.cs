using System.Collections.Generic;
using Newtonsoft.Json;
using MediaRenamer.TMDb.Models.Search;

namespace MediaRenamer.TMDb.Models.Find
{
	public class FindContainer
	{
		[JsonProperty("movie_results")]
		public List<SearchMovie> MovieResults { get; set; }

		[JsonProperty("person_results")]
		public List<FindPerson> PersonResults { get; set; } // Unconfirmed type

		[JsonProperty("tv_episode_results")]
		public List<SearchTvEpisode> TvEpisode { get; set; }

		[JsonProperty("tv_results")]
		public List<SearchTv> TvResults { get; set; }

		[JsonProperty("tv_season_results")]
		public List<FindTvSeason> TvSeason { get; set; }
	}
}