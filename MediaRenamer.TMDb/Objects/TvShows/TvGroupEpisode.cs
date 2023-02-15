using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.TvShows
{
	public class TvGroupEpisode : TvEpisodeBase
	{
		[JsonProperty("order")]
		public int Order { get; set; }
	}
}