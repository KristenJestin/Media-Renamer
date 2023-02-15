﻿using MediaRenamer.TMDb.Utilities;

namespace MediaRenamer.TMDb.Models.TvShows
{
	public enum TvShowListType
	{
		[EnumValue("on_the_air")]
		OnTheAir,
		[EnumValue("airing_today")]
		AiringToday,
		[EnumValue("top_rated")]
		TopRated,
		[EnumValue("popular")]
		Popular
	}
}