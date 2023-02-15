using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.Search
{
	public class AccountSearchTv : SearchTv
	{
		[JsonProperty("rating")]
		public float Rating { get; set; }
	}
}