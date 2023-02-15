using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.General
{
	public class TranslationsContainerTv
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("translations")]
		public List<Translation> Translations { get; set; }
	}
}