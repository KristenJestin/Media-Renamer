using System.Collections.Generic;
using Newtonsoft.Json;
using MediaRenamer.TMDb.Models.General;

namespace MediaRenamer.TMDb.Models.Search
{
	public class SearchPerson : SearchBase
	{
		public SearchPerson()
		{
			MediaType = MediaType.Person;
		}

		[JsonProperty("adult")]
		public bool Adult { get; set; }

		[JsonProperty("known_for")]
		public List<KnownForBase> KnownFor { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("profile_path")]
		public string ProfilePath { get; set; }
	}
}