using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.General
{
	public class TMDbConfig
	{
		[JsonProperty("change_keys")]
		public List<string> ChangeKeys { get; set; }

		[JsonProperty("images")]
		public ConfigImageTypes Images { get; set; }
	}
}