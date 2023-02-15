using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.Search
{
	public class SearchTvShowWithRating : SearchTv
	{
		[JsonProperty("rating")]
		public double Rating { get; set; }
	}
}