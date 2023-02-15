using MediaRenamer.TMDb.Utilities;

namespace MediaRenamer.TMDb.Models.General
{
	public enum CreditType
	{
		Unknown,

		[EnumValue("crew")]
		Crew = 1,

		[EnumValue("cast")]
		Cast = 2
	}
}