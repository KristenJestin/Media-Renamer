using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.TvShows
{
	public class TvEpisodeAccountState : TvAccountState
	{
		/// <summary>
		/// The TMDb if for the related movie
		/// </summary>
		[JsonProperty("id")]
		public int Id { get; set; }
	}
}
