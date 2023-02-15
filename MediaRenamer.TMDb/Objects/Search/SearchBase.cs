using Newtonsoft.Json;
using MediaRenamer.TMDb.Objects.General;

namespace MediaRenamer.TMDb.Objects.Search
{
	public class SearchBase
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("media_type")]
		public MediaType MediaType { get; set; }

		[JsonProperty("popularity")]
		public double Popularity { get; set; }
	}
}