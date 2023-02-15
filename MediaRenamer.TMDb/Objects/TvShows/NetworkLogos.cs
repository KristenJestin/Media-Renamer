using System.Collections.Generic;
using Newtonsoft.Json;
using MediaRenamer.TMDb.Objects.General;

namespace MediaRenamer.TMDb.Objects.TvShows
{
	public class NetworkLogos
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("logos")]
		public List<ImageData> Logos { get; set; }
	}
}