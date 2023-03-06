using Flurl.Http;
using MediaRenamer.TMDb.Models.Discover;
using MediaRenamer.TMDb.Models.General;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRenamer.TMDb.Client
{
    public partial class TMDbClient
    {
        /// <summary>
        /// Can be used to discover movies matching certain criteria
        /// </summary>
        public DiscoverMovie DiscoverMoviesAsync()
            => new DiscoverMovie(this);

        internal async Task<SearchContainer<T>> DiscoverPerformAsync<T>(string endpoint, string? language, int page, Dictionary<string, string> parameters, CancellationToken cancellationToken = default)
        {
            var request = GetUrl()
                .AppendPathSegment(endpoint);

            if (page > 1)
                request.SetQueryParam("page", page.ToString());

            if (!string.IsNullOrWhiteSpace(language))
                request.SetQueryParam("language", language);

            foreach (KeyValuePair<string, string> pair in parameters)
                request.SetQueryParam(pair.Key, pair.Value);

            return await request.GetJsonAsync<SearchContainer<T>>(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Can be used to discover new tv shows matching certain criteria
        /// </summary>
        public DiscoverTv DiscoverTvShowsAsync()
            => new DiscoverTv(this);
    }
}