using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.General
{
	public class Job
	{
		[JsonProperty("department")]
		public string Department { get; set; }

		[JsonProperty("jobs")]
		public List<string> Jobs { get; set; }
	}
}
