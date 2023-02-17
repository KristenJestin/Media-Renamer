using MediaRenamer.TMDb.Models.General;
using MediaRenamer.TMDb.Models.TvShows;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Flurl.Http;
using MediaRenamer.TMDb.Utilities;
using System.Linq;

namespace MediaRenamer.TMDb.Client
{
    public partial class TMDbClient
    {
        private async Task<T> GetTvEpisodeMethodInternal<T>(int tvShowId, int seasonNumber, int episodeNumber, TvEpisodeMethods tvShowMethod, string? language = null, CancellationToken cancellationToken = default) where T : new()
        {

            var req = GetUrl()
                .AppendPathSegments(
                    "tv", tvShowId.ToString(CultureInfo.InvariantCulture),
                    "season", seasonNumber.ToString(CultureInfo.InvariantCulture),
                    "episode", episodeNumber.ToString(CultureInfo.InvariantCulture)
                );

            if (tvShowMethod != TvEpisodeMethods.Undefined)
                req.SetQueryParam("append_to_response", string.Join(",", 
                    Enum.GetValues(typeof(TvEpisodeMethods)).OfType<TvEpisodeMethods>().Except(new[] { TvEpisodeMethods.Undefined }).Where(s => tvShowMethod.HasFlag(s)).Select(s => s.GetDescription())));

            // TODO: Dateformat?
            //if (dateFormat != null)
            //    req.DateFormat = dateFormat;

            language ??= DefaultLanguage;
            if (!string.IsNullOrWhiteSpace(language))
                req.SetQueryParam("language", language);

            return await req.GetJsonAsync<T>(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a specific episode using TMDb id of the associated tv show.
        /// </summary>
        /// <param name="tvShowId">TMDb id of the tv show the desired episode belongs to.</param>
        /// <param name="seasonNumber">The season number of the season the episode belongs to. Note use 0 for specials.</param>
        /// <param name="episodeNumber">The episode number of the episode you want to retrieve.</param>
        /// <param name="extraMethods">Enum flags indicating any additional data that should be fetched in the same request.</param>
        /// <param name="language">If specified the api will attempt to return a localized result. ex: en,it,es </param>
        /// <param name="includeImageLanguage">If specified the api will attempt to return localized image results eg. en,it,es.</param>
        /// <param name="cancellationToken">A cancellation token</param>
        public async Task<TvEpisode> GetTvEpisodeAsync(int tvShowId, int seasonNumber, int episodeNumber, TvEpisodeMethods extraMethods = TvEpisodeMethods.Undefined, string? language = null, CancellationToken cancellationToken = default)
            => await GetTvEpisodeMethodInternal<TvEpisode>(tvShowId, seasonNumber, episodeNumber, extraMethods, language, cancellationToken).ConfigureAwait(false);
    }
}