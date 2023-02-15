using Newtonsoft.Json;
using MediaRenamer.TMDb.Utilities;
using MediaRenamer.TMDb.Utilities.Converters;

namespace MediaRenamer.TMDb.Models.General
{
	[JsonConverter(typeof(EnumStringValueConverter))]
	public enum MediaType
	{
		Unknown,

		[EnumValue("movie")]
		Movie = 1,

		[EnumValue("tv")]
		Tv = 2,

		[EnumValue("person")]
		Person = 3
	}
}