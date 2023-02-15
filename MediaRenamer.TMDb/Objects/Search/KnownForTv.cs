using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using MediaRenamer.TMDb.Objects.General;

namespace MediaRenamer.TMDb.Objects.Search
{
	public class KnownForTv : KnownForBase
	{
		public KnownForTv()
		{
			MediaType = MediaType.Tv;
		}

		[JsonProperty("first_air_date")]
		public DateTime? FirstAirDate { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("original_name")]
		public string OriginalName { get; set; }

		[JsonProperty("origin_country")]
		public List<string> OriginCountry { get; set; }
	}
}