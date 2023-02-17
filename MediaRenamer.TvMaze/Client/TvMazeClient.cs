using Flurl;
using System;

namespace MediaRenamer.TvMaze.Client
{
    public partial class TvMazeClient
    {
        private readonly Uri Url = new Uri("https://api.tvmaze.com");

        public TvMazeClient()
        {
        }

        public Url GetUrl()
            => Url;
    }
}
