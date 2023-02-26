using System;

namespace MediaRenamer.Media.Models
{
    public class MediaData
    {
        public string? ExternalId { get; private set; }
        public string Title { get; private set; }
        public DateTime? ReleaseDate { get; private set; }
        public string? EpisodeTitle { get; private set; }
        public int? Season { get; private set; }
        public int? Episode { get; private set; }
        public string? Collection { get; private set; }

        public MediaData(string title, DateTime? releaseDate, string? externalId)
        {
            Title = title;
            ReleaseDate = releaseDate;
            ExternalId = externalId;
        }


        #region methods
        public MediaData WithTvInfos(string episodeTitle, int season, int episode)
        {
            EpisodeTitle = episodeTitle;
            Season = season;
            Episode = episode;
            return this;
        }
        public MediaData WithMovieInfos(string? collection)
        {
            Collection = collection;
            return this;
        }
        #endregion
    }
}
