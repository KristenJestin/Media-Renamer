using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.General
{
	public class ExternalIdsTvEpisode : ExternalIds
	{
		[JsonProperty("imdb_id")]
		public string ImdbId { get; set; }

		[JsonProperty("tvdb_id")]
		public string TvdbId { get; set; }
	}
}