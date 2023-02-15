using System.Collections.Generic;
using Newtonsoft.Json;
using MediaRenamer.TMDb.Objects.Search;

namespace MediaRenamer.TMDb.Objects.Lists
{
	public class GenericList : TMDbList<string>
	{
		[JsonProperty("created_by")]
		public string CreatedBy { get; set; }

		[JsonProperty("items")]
		public List<SearchMovie> Items { get; set; }
	}
}