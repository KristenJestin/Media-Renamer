using Newtonsoft.Json;
using MediaRenamer.TMDb.Models.General;

namespace MediaRenamer.TMDb.Models.Lists
{
	public class AccountList : TMDbList<int>
	{
		[JsonProperty("list_type")]
		public MediaType ListType { get; set; }
	}
}