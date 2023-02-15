using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.TvShows
{
	public class TvAccountState
	{
		[JsonProperty("rating")]
		public double? Rating { get; set; }
	}
}