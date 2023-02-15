using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.General
{
	public class ImagesWithId : Images
	{
		[JsonProperty("id")]
		public int Id { get; set; }
	}
}