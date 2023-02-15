using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.Search
{
	public class AccountSearchTv : SearchTv
	{
		[JsonProperty("rating")]
		public float Rating { get; set; }
	}
}