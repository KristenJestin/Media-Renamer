using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.Search
{
	public class AccountSearchTvEpisode : SearchTvEpisode
	{
		[JsonProperty("rating")]
		public double Rating { get; set; }
	}
}