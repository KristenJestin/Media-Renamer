using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.General
{
	public class TranslationsContainer
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("translations")]
		public List<Translation> Translations { get; set; }
	}
}