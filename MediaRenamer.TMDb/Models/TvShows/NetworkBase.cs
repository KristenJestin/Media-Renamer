using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.TvShows
{
	public class NetworkBase
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("origin_country")]
		public string OriginCountry { get; set; }
	}
}