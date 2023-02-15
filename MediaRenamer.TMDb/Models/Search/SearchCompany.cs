using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.Search
{
	public class SearchCompany
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("logo_path")]
		public string LogoPath { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}