using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.General
{
	public class SearchContainerWithDates<T> : SearchContainer<T>
	{
		[JsonProperty("dates")]
		public DateRange Dates { get; set; }
	}
}