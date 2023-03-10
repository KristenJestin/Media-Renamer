using System;
using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.Search
{
	public class SearchTvEpisode
	{
		[JsonProperty("air_date")]
		public DateTime? AirDate { get; set; }

		[JsonProperty("episode_number")]
		public int EpisodeNumber { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("overview")]
		public string Overview { get; set; }

		[JsonProperty("production_code")]
		public string ProductionCode { get; set; }

		[JsonProperty("season_number")]
		public int SeasonNumber { get; set; }

		[JsonProperty("show_id")]
		public int ShowId { get; set; }

		[JsonProperty("still_path")]
		public string StillPath { get; set; }

		[JsonProperty("vote_average")]
		public double VoteAverage { get; set; }

		[JsonProperty("vote_count")]
		public int VoteCount { get; set; }
	}
}