using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.Exceptions
{
	public class TMDbStatusMessage
	{
		[JsonProperty("status_code")]
		public int StatusCode { get; set; }

		[JsonProperty("status_message")]
		public string StatusMessage { get; set; }
	}
}