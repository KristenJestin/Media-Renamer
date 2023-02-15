using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.General
{
	public class SingleResultContainer<T>
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("results")]
		public T Results { get; set; }
	}
}