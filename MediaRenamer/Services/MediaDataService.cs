using MediaRenamer.Models.Medias;
using MediaRenamer.TMDb.Client;
using MediaRenamer.TMDb.Objects.General;
using MediaRenamer.TMDb.Objects.Search;

namespace MediaRenamer.Services;

public class MediaDataService
{
    private readonly TMDbClient _client;

    public MediaDataService(TMDbClient client)
    {
        _client = client;
    }

    public async Task<SearchContainer<SearchBase>> SearchMultiAsync(MediaData media)
        => await _client.SearchMultiAsync(media.Data.Title);

    public async Task<SearchContainer<SearchMovie>> SearchMovieAsync(MediaData media)
        => await _client.SearchMovieAsync(media.Data.Title, year: media.Data.Year);

    public async Task<SearchContainer<SearchTv>> SearchTvAsync(MediaData media)
        => await _client.SearchTvShowAsync(media.Data.Title, firstAirDateYear: media.Data.Year);
}
