using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.General
{
	public class Keyword
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}