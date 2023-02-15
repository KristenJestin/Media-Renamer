using System;

namespace MediaRenamer.TMDb.Objects.Exceptions
{
	public class TMDbException : Exception
	{
		public TMDbException(string message)
				: base(message)
		{
		}
	}
}