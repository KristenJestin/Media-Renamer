using System.Collections.Generic;
using Newtonsoft.Json;
using MediaRenamer.TMDb.Models.General;
using MediaRenamer.TMDb.Utilities.Converters;

namespace MediaRenamer.TMDb.Models.Search
{
	public abstract class KnownForBase
	{
		[JsonProperty("backdrop_path")]
		public string BackdropPath { get; set; }

		[JsonProperty("genre_ids")]
		public List<int> GenreIds { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("media_type")]
		public MediaType MediaType { get; set; }

		[JsonProperty("original_language")]
		public string OriginalLanguage { get; set; }

		[JsonProperty("overview")]
		public string Overview { get; set; }

		[JsonProperty("popularity")]
		public double Popularity { get; set; }

		[JsonProperty("poster_path")]
		public string PosterPath { get; set; }

		[JsonProperty("vote_average")]
		public double VoteAverage { get; set; }

		[JsonProperty("vote_count")]
		public int VoteCount { get; set; }
	}
}