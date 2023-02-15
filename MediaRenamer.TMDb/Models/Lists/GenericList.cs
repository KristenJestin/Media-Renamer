using System.Collections.Generic;
using Newtonsoft.Json;
using MediaRenamer.TMDb.Models.Search;

namespace MediaRenamer.TMDb.Models.Lists
{
	public class GenericList : TMDbList<string>
	{
		[JsonProperty("created_by")]
		public string CreatedBy { get; set; }

		[JsonProperty("items")]
		public List<SearchMovie> Items { get; set; }
	}
}