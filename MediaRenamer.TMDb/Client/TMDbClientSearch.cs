using Flurl.Http;
using MediaRenamer.TMDb.Models.General;
using MediaRenamer.TMDb.Models.Search;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRenamer.TMDb.Client
{
	public partial class TMDbClient
	{
		private async Task<T> SearchMethodInternal<T>(string method, string query, int page, string? language = null, bool? includeAdult = null, int? year = null, string? dateFormat = null, string? region = null, int primaryReleaseYear = 0, int? firstAirDateYear = null, CancellationToken cancellationToken = default) where T : new()
		{
			var req = GetUrl()
				.AppendPathSegments("search", method)
				.SetQueryParam("query", query);

			language ??= DefaultLanguage;
			if (!string.IsNullOrWhiteSpace(language))
				req.SetQueryParam("language", language);

			if (page >= 1)
				req.SetQueryParam("page", page.ToString());
			if (year.HasValue && year.Value >= 1)
				req.SetQueryParam("year", year.Value.ToString());
			if (includeAdult.HasValue)
				req.SetQueryParam("include_adult", includeAdult.Value ? "true" : "false");

			// TODO: Dateformat?
			//if (dateFormat != null)
			//    req.DateFormat = dateFormat;

			if (!string.IsNullOrWhiteSpace(region))
				req.SetQueryParam("region", region);

			if (primaryReleaseYear >= 1)
				req.SetQueryParam("primary_release_year", primaryReleaseYear.ToString());
			if (firstAirDateYear.HasValue && firstAirDateYear.Value >= 1)
				req.SetQueryParam("first_air_date_year", firstAirDateYear.Value.ToString());

			return await req.GetJsonAsync<T>(cancellationToken).ConfigureAwait(false);
		}

		public async Task<SearchContainer<SearchCollection>> SearchCollectionAsync(string query, int page = 0, CancellationToken cancellationToken = default)
			=> await SearchCollectionAsync(query, DefaultLanguage, page, cancellationToken).ConfigureAwait(false);

		public async Task<SearchContainer<SearchCollection>> SearchCollectionAsync(string query, string? language, int page = 0, CancellationToken cancellationToken = default)
			=> await SearchMethodInternal<SearchContainer<SearchCollection>>("collection", query, page, language, cancellationToken: cancellationToken).ConfigureAwait(false);

		public async Task<SearchContainer<SearchCompany>> SearchCompanyAsync(string query, int page = 0, CancellationToken cancellationToken = default)
			=> await SearchMethodInternal<SearchContainer<SearchCompany>>("company", query, page, cancellationToken: cancellationToken).ConfigureAwait(false);

		public async Task<SearchContainer<SearchKeyword>> SearchKeywordAsync(string query, int page = 0, CancellationToken cancellationToken = default)
			=> await SearchMethodInternal<SearchContainer<SearchKeyword>>("keyword", query, page, cancellationToken: cancellationToken).ConfigureAwait(false);

		public async Task<SearchContainer<SearchMovie>> SearchMovieAsync(string query, int page = 0, bool includeAdult = false, int? year = null, string? region = null, int primaryReleaseYear = 0, CancellationToken cancellationToken = default)
			=> await SearchMovieAsync(query, DefaultLanguage, page, includeAdult, year, region, primaryReleaseYear, cancellationToken).ConfigureAwait(false);

		public async Task<SearchContainer<SearchMovie>> SearchMovieAsync(string query, string? language, int page = 0, bool includeAdult = false, int? year = null, string? region = null, int primaryReleaseYear = 0, CancellationToken cancellationToken = default)
			=> await SearchMethodInternal<SearchContainer<SearchMovie>>("movie", query, page, language, includeAdult, year, "yyyy-MM-dd", region, primaryReleaseYear, cancellationToken: cancellationToken).ConfigureAwait(false);

		public async Task<SearchContainer<SearchBase>> SearchMultiAsync(string query, int page = 0, bool includeAdult = false, int? year = null, string? region = null, CancellationToken cancellationToken = default)
			=> await SearchMultiAsync(query, DefaultLanguage, page, includeAdult, year, region, cancellationToken).ConfigureAwait(false);

		public async Task<SearchContainer<SearchBase>> SearchMultiAsync(string query, string? language, int page = 0, bool includeAdult = false, int? year = null, string? region = null, CancellationToken cancellationToken = default)
			=> await SearchMethodInternal<SearchContainer<SearchBase>>("multi", query, page, language, includeAdult, year, "yyyy-MM-dd", region, cancellationToken: cancellationToken).ConfigureAwait(false);

		public async Task<SearchContainer<SearchTv>> SearchTvShowAsync(string query, int page = 0, bool includeAdult = false, int? firstAirDateYear = null, CancellationToken cancellationToken = default)
			=> await SearchTvShowAsync(query, DefaultLanguage, page, includeAdult, firstAirDateYear, cancellationToken).ConfigureAwait(false);

		public async Task<SearchContainer<SearchTv>> SearchTvShowAsync(string query, string? language, int page = 0, bool includeAdult = false, int? firstAirDateYear = null, CancellationToken cancellationToken = default)
			=> await SearchMethodInternal<SearchContainer<SearchTv>>("tv", query, page, language, includeAdult, firstAirDateYear: firstAirDateYear, cancellationToken: cancellationToken).ConfigureAwait(false);
	}
}
