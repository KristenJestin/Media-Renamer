using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.Search
{
	public class SearchKeyword
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}