using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.TvShows
{
	public class TvEpisodeAccountStateWithNumber : TvEpisodeAccountState
	{
		[JsonProperty("episode_number")]
		public int EpisodeNumber { get; set; }
	}
}