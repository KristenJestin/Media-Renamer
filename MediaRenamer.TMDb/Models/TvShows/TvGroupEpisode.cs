﻿using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.TvShows
{
	public class TvGroupEpisode : TvEpisodeBase
	{
		[JsonProperty("order")]
		public int Order { get; set; }
	}
}