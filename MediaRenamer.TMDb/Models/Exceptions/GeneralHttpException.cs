using System.Net;

namespace MediaRenamer.TMDb.Models.Exceptions
{
	public class GeneralHttpException : TMDbException
	{
		public HttpStatusCode HttpStatusCode { get; }

		public GeneralHttpException(HttpStatusCode httpStatusCode)
			: base("TMDb returned an unexpected HTTP error")
		{
			HttpStatusCode = httpStatusCode;
		}
	}
}