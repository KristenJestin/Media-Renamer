using System;
using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.General
{
	public class DateRange
	{
		[JsonProperty("maximum")]
		public DateTime Maximum { get; set; }

		[JsonProperty("minimum")]
		public DateTime Minimum { get; set; }
	}
}