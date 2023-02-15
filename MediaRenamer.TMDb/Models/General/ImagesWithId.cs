using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.General
{
	public class ImagesWithId : Images
	{
		[JsonProperty("id")]
		public int Id { get; set; }
	}
}