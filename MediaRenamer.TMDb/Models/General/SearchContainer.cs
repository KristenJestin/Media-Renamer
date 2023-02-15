﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.General
{
	public class SearchContainer<T>
	{
		[JsonProperty("page")]
		public int Page { get; set; }

		[JsonProperty("results")]
		public List<T> Results { get; set; }

		[JsonProperty("total_pages")]
		public int TotalPages { get; set; }

		[JsonProperty("total_results")]
		public int TotalResults { get; set; }
	}
}