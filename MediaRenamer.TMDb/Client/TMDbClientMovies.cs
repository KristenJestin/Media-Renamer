using Flurl.Http;
using MediaRenamer.TMDb.Models.Movies;
using MediaRenamer.TMDb.Utilities;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRenamer.TMDb.Client
{
    public partial class TMDbClient
    {
        private async Task<T> GetMovieMethodInternal<T>(int movieId, MovieMethods movieMethod, string? language = null, string? includeImageLanguage = null, MovieMethods extraMethods = MovieMethods.Undefined, CancellationToken cancellationToken = default) where T : new()
        {
            var request = GetUrl()
                .AppendPathSegments("movie", movieId.ToString(CultureInfo.InvariantCulture));

            if (movieMethod != MovieMethods.Undefined)
                request.AppendPathSegment(movieMethod.GetDescription());

            language ??= DefaultLanguage;
            if (!string.IsNullOrWhiteSpace(language))
                request.SetQueryParam("language", language);

            if (!string.IsNullOrWhiteSpace(includeImageLanguage))
                request.SetQueryParam("include_image_language", includeImageLanguage);

            var appends = string.Join(",",
                Enum.GetValues(typeof(MovieMethods))
                    .OfType<MovieMethods>()
                    .Except(new[] { MovieMethods.Undefined })
                    .Where(s => extraMethods.HasFlag(s))
                    .Select(s => s.GetDescription()));

            if (appends != string.Empty)
                request.SetQueryParam("append_to_response", appends);

            return await request.GetJsonAsync<T>(cancellationToken).ConfigureAwait(false);
        }

        public async Task<Movie> GetMovieAsync(int movieId, MovieMethods extraMethods = MovieMethods.Undefined, CancellationToken cancellationToken = default)
            => await GetMovieAsync(movieId, DefaultLanguage, null, extraMethods, cancellationToken).ConfigureAwait(false);

        public async Task<Movie> GetMovieAsync(int movieId, string? language, string? includeImageLanguage = null, MovieMethods extraMethods = MovieMethods.Undefined, CancellationToken cancellationToken = default)
            => await GetMovieMethodInternal<Movie>(movieId, MovieMethods.Undefined, language, includeImageLanguage, extraMethods, cancellationToken).ConfigureAwait(false);
    }
}
