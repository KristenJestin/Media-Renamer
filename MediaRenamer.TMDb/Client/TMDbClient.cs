using Flurl;
using Flurl.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRenamer.TMDb.Client
{
    public partial class TMDbClient
    {
        private const string ApiVersion = "3";
        private const string ProductionUrl = "api.themoviedb.org";
        private const string ImageUrl = "https://image.tmdb.org/t/p/";
        private readonly Uri Url = new Uri(string.Format("{0}://{1}/{2}/", "https", ProductionUrl, ApiVersion));

        public TMDbClient(string apiKey, string? language = null)
        {
            ApiKey = apiKey;
            DefaultLanguage = language;
        }

        public string ApiKey { get; private set; }

        /// <summary>
        /// ISO 3166-1 code. Ex. US
        /// </summary>
        public string? DefaultCountry { get; set; }

        /// <summary>
        /// ISO 639-1 code. Ex en
        /// </summary>
        public string? DefaultLanguage { get; set; }

        /// <summary>
        /// ISO 639-1 code. Ex en
        /// </summary>
        public string? DefaultImageLanguage { get; set; }

        public Url GetUrl()
            => Url.SetQueryParam("api_key", ApiKey);

        public Uri GetImageUrl(string size, string filePath)
            => new Uri(ImageUrl + size + filePath);

        public async Task<byte[]> GetImageBytes(string size, string filePath, CancellationToken token = default)
        {
            var url = GetImageUrl(size, filePath);
            return await url.GetBytesAsync().ConfigureAwait(false);
        }
    }
}