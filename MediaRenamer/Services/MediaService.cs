using MediaRenamer.Media.Models;
using MediaRenamer.TMDb.Client;
using MediaRenamer.TMDb.Models.General;
using MediaRenamer.TMDb.Models.Search;
using MediaRenamer.TvMaze.Client;
using MediaRenamer.TvMaze.Models.Search;
using MediaRenamer.TvMaze.Models.Shows;
using System.Globalization;

namespace MediaRenamer.Services;

public class MediaService
{
    private readonly TMDbClient _tmdbClient;
    private readonly TvMazeClient _tvmazeClient;

    public MediaService(TMDbClient tmdbclient, TvMazeClient tvmazeClient)
    {
        _tmdbClient = tmdbclient;
        _tvmazeClient = tvmazeClient;
    }

    public async Task<SearchContainer<SearchMovie>> SearchMovieAsync(MediaFile media)
        => await _tmdbClient.SearchMovieAsync(media.ExtractedData.Title, year: media.ExtractedData.Year);

    public async Task<ShowWithEpisodes?> SearchTvAsync(MediaFile media)
        => await _tvmazeClient.SingleSearchShow(media.ExtractedData.Title, embedEpisodes: true);

    public async Task<MediaData?> SearchTvMovieAsync(MediaFile media)
    {
        if (media.ExtractedData.Type == Media.Models.MediaType.Tv)
        {
            var result = await SearchTvAsync(media);
            if (result == null)
                return null;

            Episode? episode = null;

            var data = new MediaData(result.Name, DateTime.TryParseExact(result.Premiered, "yyyy-MM-dd", provider: null, DateTimeStyles.None, out var date) ? date : null);
            if (media.ExtractedData.Season == null || media.ExtractedData.Season < 1)
            {
                if (!media.ExtractedData.Episode.HasValue)
                    return null;

                // absolute episode
                episode = result.Embededs.Episodes[media.ExtractedData.Episode.Value - 1];
            }
            else
            {
                episode = result.Embededs.Episodes.Find(episode => episode.Season == media.ExtractedData.Season && episode.Number == media.ExtractedData.Episode);
            }

            // check if it has been found
            if (episode == null || !episode.Season.HasValue || !episode.Number.HasValue)
                return null;

            return data.WithTvInfos(episode.Name, episode.Season.Value, episode.Number.Value);
        }
        else if (media.ExtractedData.Type == Media.Models.MediaType.Movie)
        {
            var results = await SearchMovieAsync(media);
            var result = results.Results.FirstOrDefault();
            if (result == null)
                return null;

            return new MediaData(result.Title, result.ReleaseDate);
        }

        return null;
    }
}
