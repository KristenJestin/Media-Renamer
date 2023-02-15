using MediaRenamer.TMDb.Objects.Search;
using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Objects.Find
{
	public class FindPerson : SearchPerson
	{
		[JsonProperty("known_for_department")]
		public string KnownForDepartment { get; set; }
	}
}