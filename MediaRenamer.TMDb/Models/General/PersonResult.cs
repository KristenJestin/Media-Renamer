using System.Collections.Generic;
using Newtonsoft.Json;
using MediaRenamer.TMDb.Models.Search;

namespace MediaRenamer.TMDb.Models.General
{
	public class PersonResult
	{
		[JsonProperty("adult")]
		public bool Adult { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("known_for")]
		public List<KnownForBase> KnownFor { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("popularity")]
		public double Popularity { get; set; }

		[JsonProperty("profile_path")]
		public string ProfilePath { get; set; }
	}
}