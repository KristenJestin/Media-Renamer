using MediaRenamer.TMDb.Utilities;

namespace MediaRenamer.TMDb.Models.General
{
	public enum SortOrder
	{
		Undefined = 0,
		[EnumValue("asc")]
		Ascending = 1,
		[EnumValue("desc")]
		Descending = 2
	}
}
