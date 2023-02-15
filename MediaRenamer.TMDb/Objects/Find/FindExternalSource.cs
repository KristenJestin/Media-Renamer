using MediaRenamer.TMDb.Utilities;

namespace MediaRenamer.TMDb.Objects.Find
{
	public enum FindExternalSource
	{
		[EnumValue("imdb_id")]
		Imdb,

		[EnumValue("tvdb_id")]
		TvDb
	}
}