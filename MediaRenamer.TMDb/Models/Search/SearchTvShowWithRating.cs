using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.Search
{
	public class SearchTvShowWithRating : SearchTv
	{
		[JsonProperty("rating")]
		public double Rating { get; set; }
	}
}