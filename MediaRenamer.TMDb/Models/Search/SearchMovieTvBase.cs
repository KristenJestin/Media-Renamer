using Newtonsoft.Json;
using System.Collections.Generic;

namespace MediaRenamer.TMDb.Models.Search
{
	public class SearchMovieTvBase : SearchBase
	{
		[JsonProperty("backdrop_path")]
		public string BackdropPath { get; set; }

		[JsonProperty("genre_ids")]
		public List<int> GenreIds { get; set; }

		[JsonProperty("original_language")]
		public string OriginalLanguage { get; set; }

		[JsonProperty("overview")]
		public string Overview { get; set; }

		[JsonProperty("poster_path")]
		public string PosterPath { get; set; }

		[JsonProperty("vote_average")]
		public double VoteAverage { get; set; }

		[JsonProperty("vote_count")]
		public int VoteCount { get; set; }
	}
}