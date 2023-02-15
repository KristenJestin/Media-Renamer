using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.General
{
	public class ResultContainer<T>
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("results")]
		public List<T> Results { get; set; }
	}
}