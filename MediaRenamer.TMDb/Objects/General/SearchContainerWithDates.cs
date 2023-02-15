using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.General
{
	public class SearchContainerWithDates<T> : SearchContainer<T>
	{
		[JsonProperty("dates")]
		public DateRange Dates { get; set; }
	}
}