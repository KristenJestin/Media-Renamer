using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.General
{
	public class AlternativeName
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }
	}
}