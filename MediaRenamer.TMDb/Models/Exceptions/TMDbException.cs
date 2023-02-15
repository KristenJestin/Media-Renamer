using System;

namespace MediaRenamer.TMDb.Models.Exceptions
{
	public class TMDbException : Exception
	{
		public TMDbException(string message)
				: base(message)
		{
		}
	}
}