using Newtonsoft.Json;
using MediaRenamer.TMDb.Objects.General;

namespace MediaRenamer.TMDb.Objects.Lists
{
	public class AccountList : TMDbList<int>
	{
		[JsonProperty("list_type")]
		public MediaType ListType { get; set; }
	}
}