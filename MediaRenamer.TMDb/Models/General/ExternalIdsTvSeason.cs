using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.General
{
	public class ExternalIdsTvSeason : ExternalIds
	{
		[JsonProperty("tvdb_id")]
		public string TvdbId { get; set; }
	}
}