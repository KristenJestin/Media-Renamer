using MediaRenamer.TMDb.Utilities;

namespace MediaRenamer.TMDb.Models.Find
{
	public enum FindExternalSource
	{
		[EnumValue("imdb_id")]
		Imdb,

		[EnumValue("tvdb_id")]
		TvDb
	}
}