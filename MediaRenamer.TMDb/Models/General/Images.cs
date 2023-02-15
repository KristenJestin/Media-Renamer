using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.General
{
	public class Images
	{
		[JsonProperty("backdrops")]
		public List<ImageData> Backdrops { get; set; }

		[JsonProperty("posters")]
		public List<ImageData> Posters { get; set; }
	}
}