using MediaRenamer.TMDb.Models.Search;
using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.Find
{
	public class FindPerson : SearchPerson
	{
		[JsonProperty("known_for_department")]
		public string KnownForDepartment { get; set; }
	}
}