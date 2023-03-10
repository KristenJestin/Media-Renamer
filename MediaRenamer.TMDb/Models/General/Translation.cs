using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.General
{
	public class Translation
	{
		[JsonProperty("english_name")]
		public string EnglishName { get; set; }

		/// <summary>
		/// A language code, e.g. en
		/// </summary>
		[JsonProperty("iso_639_1")]
		public string Iso_639_1 { get; set; }

		[JsonProperty("iso_3166_1")]
		public string Iso_3166_1 { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}