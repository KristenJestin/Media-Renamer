using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.TvShows
{
	public class TvAccountState
	{
		[JsonProperty("rating")]
		public double? Rating { get; set; }
	}
}