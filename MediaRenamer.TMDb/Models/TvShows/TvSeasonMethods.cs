using System;
using MediaRenamer.TMDb.Utilities;

namespace MediaRenamer.TMDb.Models.TvShows
{
	[Flags]
	public enum TvSeasonMethods
	{
		[EnumValue("Undefined")]
		Undefined = 0,
		[EnumValue("credits")]
		Credits = 1,
		[EnumValue("images")]
		Images = 2,
		[EnumValue("external_ids")]
		ExternalIds = 4,
		[EnumValue("videos")]
		Videos = 8,
		[EnumValue("account_states")]
		AccountStates = 16,
	}
}
