using MediaRenamer.TMDb.Utilities;

namespace MediaRenamer.TMDb.Objects.General
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