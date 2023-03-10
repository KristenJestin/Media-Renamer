using Newtonsoft.Json;
using System.Collections.Generic;

namespace MediaRenamer.TMDb.Models.General
{
	public class AlternativeNames
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("results")]
		public List<AlternativeName> Results { get; set; }
	}
}