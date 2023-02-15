using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.General
{
	public class ExternalIdsTvEpisode : ExternalIds
	{
		[JsonProperty("imdb_id")]
		public string ImdbId { get; set; }

		[JsonProperty("tvdb_id")]
		public string TvdbId { get; set; }
	}
}