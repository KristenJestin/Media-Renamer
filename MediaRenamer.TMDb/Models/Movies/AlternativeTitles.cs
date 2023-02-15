using System.Collections.Generic;
using Newtonsoft.Json;
using MediaRenamer.TMDb.Models.General;

namespace MediaRenamer.TMDb.Models.Movies
{
	public class AlternativeTitles
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("titles")]
		public List<AlternativeTitle> Titles { get; set; }
	}
}