using System;

namespace MediaRenamer.Media.Models
{
    public class MediaData
    {
        public string? ExternalId { get; private set; }
        public string? ExternalProvider{ get; private set; }
        public string Title { get; private set; }
        public DateTime? ReleaseDate { get; private set; }
        public string? EpisodeTitle { get; private set; }
        public int? Season { get; private set; }
        public int? Episode { get; private set; }
        public MediaDataCollection? Collection { get; private set; }

        public MediaData(string title, DateTime? releaseDate, string? externalId, string? externalProvider)
        {
            Title = title;
            ReleaseDate = releaseDate?.ToUniversalTime();
            ExternalId = externalId;
            ExternalProvider = externalProvider;
        }


        #region methods
        public MediaData WithTvInfos(string episodeTitle, int season, int episode)
        {
            EpisodeTitle = episodeTitle;
            Season = season;
            Episode = episode;
            return this;
        }
        public MediaData WithMovieInfos(string collectionId, string collectionName, string? collectionPoster)
        {
            Collection = new MediaDataCollection
            {
                ExternalId = collectionId,
                Name = collectionName,
                Poster = collectionPoster
            };
            return this;
        }
        #endregion
    }

    public class MediaDataCollection
    {
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string? Poster { get; set; }
    }
}
