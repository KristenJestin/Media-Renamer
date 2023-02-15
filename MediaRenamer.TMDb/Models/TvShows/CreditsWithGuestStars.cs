using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaRenamer.TMDb.Models.TvShows
{
	public class CreditsWithGuestStars : Credits
	{
		[JsonProperty("guest_stars")]
		public List<Cast> GuestStars { get; set; }
	}
}