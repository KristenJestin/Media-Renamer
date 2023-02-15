namespace MediaRenamer.TMDb.Objects.General
{
	public class SearchContainerWithId<T> : SearchContainer<T>
	{
		public int Id { get; set; }
	}
}