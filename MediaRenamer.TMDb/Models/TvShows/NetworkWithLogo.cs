using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.TvShows
{
	public class NetworkWithLogo : NetworkBase
	{
		[JsonProperty("logo_path")]
		public string LogoPath { get; set; }
	}
}
