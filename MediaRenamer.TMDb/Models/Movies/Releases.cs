using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.Movies
{
	public class Releases
	{
		[JsonProperty("countries")]
		public List<Country> Countries { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }
	}
}