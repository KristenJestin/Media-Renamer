using MediaRenamer.TMDb.Client;
using MediaRenamer.TMDb.Models.General;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRenamer.TMDb.Models.Discover
{
    public abstract class DiscoverBase<T>
    {
        private readonly TMDbClient _client;
        private readonly string _endpoint;
        protected Dictionary<string, string> Parameters { get; set; }

        public DiscoverBase(string endpoint, TMDbClient client)
        {
            _endpoint = endpoint;
            _client = client;
            Parameters = new Dictionary<string, string>();
        }

        public async Task<SearchContainer<T>> Query(int page = 0, CancellationToken cancellationToken = default)
            => await Query(_client.DefaultLanguage, page, cancellationToken).ConfigureAwait(false);

        public async Task<SearchContainer<T>> Query(string? language, int page = 0, CancellationToken cancellationToken = default)
            => await _client.DiscoverPerformAsync<T>(_endpoint, language, page, Parameters, cancellationToken).ConfigureAwait(false);
    }
}