using Flurl.Http;
using MediaRenamer.TvMaze.Models.Search;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRenamer.TvMaze.Client
{
    public partial class TvMazeClient
    {
        public async Task<ShowWithEpisodes?> SingleSearchShow(string query, bool embedEpisodes = false, CancellationToken cancellationToken = default)
        {
            var req = GetUrl()
                .AppendPathSegments("singlesearch", "shows")
                .SetQueryParam("q", query);

            if (embedEpisodes)
                req.SetQueryParam("embed", "episodes");

            return await req.GetJsonAsync<ShowWithEpisodes>(cancellationToken).ConfigureAwait(false);
        }
    }
}
