using System.Collections.Generic;
using Newtonsoft.Json;
using MediaRenamer.TMDb.Objects.General;

namespace MediaRenamer.TMDb.Objects.Movies
{
	public class KeywordsContainer
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("keywords")]
		public List<Keyword> Keywords { get; set; }
	}
}