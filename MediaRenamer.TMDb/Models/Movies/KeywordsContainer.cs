using System.Collections.Generic;
using Newtonsoft.Json;
using MediaRenamer.TMDb.Models.General;

namespace MediaRenamer.TMDb.Models.Movies
{
	public class KeywordsContainer
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("keywords")]
		public List<Keyword> Keywords { get; set; }
	}
}