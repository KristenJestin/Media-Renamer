using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.General
{
	public class PosterImages
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("posters")]
		public List<ImageData> Posters { get; set; }
	}
}