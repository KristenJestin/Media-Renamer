using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.Search
{
	public class AccountSearchTvEpisode : SearchTvEpisode
	{
		[JsonProperty("rating")]
		public double Rating { get; set; }
	}
}