using MediaRenamer.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MediaRenamer.Media.Models
{
    public class MediaParser
    {
        public IEnumerable<Func<string, MediaParserResult, MediaHandler>> Handlers { get; }


        public MediaParser(IEnumerable<Func<string, MediaParserResult, MediaHandler>> handlers)
        {
            Handlers = handlers;
        }

        public MediaParserResult Parse(string value)
        {
            var result = new MediaParserResult();
            var endOfTitle = value.Length;

            foreach (var handler in Handlers)
            {
                var handlerResult = handler(value, result);
                if (handlerResult == null || !handlerResult.Index.HasValue || handlerResult.Property == null)
                    continue;

                if (handlerResult.Index < endOfTitle)
                    endOfTitle = handlerResult.Index.Value;

                var t = Nullable.GetUnderlyingType(handlerResult.Property.PropertyType) ?? handlerResult.Property.PropertyType;
                try
                {
                    if (handlerResult.Value != null && handlerResult.Property.GetValue(result) == null)
                        handlerResult.Property?.SetValue(result, Convert.ChangeType(handlerResult.Value, t), null);
                }
                catch { }
            }

            result.Title = CleanTitle(value[..endOfTitle]);

            return result;
        }


        #region privates
        private string CleanTitle(string rawTitle)
        {
            var cleanedTitle = rawTitle;

            if (!cleanedTitle.Contains(' ', StringComparison.CurrentCulture) && cleanedTitle.IndexOf(".") != -1)
                cleanedTitle = cleanedTitle.Replace(".", " ");

            cleanedTitle = cleanedTitle.Replace("_", " ");
            cleanedTitle = Regex.Replace(cleanedTitle, "([(_]|- )$", "").Trim();

            return cleanedTitle;
        }
        #endregion

        #region statics
        public static readonly MediaParser Default = new MediaParser(new[] {
        // Year
        MediaHandler.To(r => r.Year).FromRegex(new Regex(@"(?!^)[([]?((?:19[0-9]|20[012])[0-9])[)\]]?", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Resolution
        MediaHandler.To(r => r.Resolution).FromRegex(new Regex(@"([0-9]{3,4}[pi])", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Resolution).FromRegex(new Regex(@"(4k)", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),

        // Extended
        MediaHandler.To(r => r.Extended).FromRegex(new Regex(@"EXTENDED", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Convert
        MediaHandler.To(r => r.Convert).FromRegex(new Regex(@"CONVERT", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Hardcoded
        MediaHandler.To(r => r.Hardcoded).FromRegex(new Regex(@"HC|HARDCODED", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Proper
        MediaHandler.To(r => r.Proper).FromRegex(new Regex(@"(?:REAL.)?PROPER", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Repack
        MediaHandler.To(r => r.Repack).FromRegex(new Regex(@"REPACK|RERIP", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Retail
        MediaHandler.To(r => r.Retail).FromRegex(new Regex(@"\bRetail\b", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Remastered
        MediaHandler.To(r => r.Remastered).FromRegex(new Regex(@"\bRemaster(?:ed)?\b", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Unrated
        MediaHandler.To(r => r.Unrated).FromRegex(new Regex(@"\bunrated|uncensored\b", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Region
        MediaHandler.To(r => r.Region).FromRegex(new Regex(@"R[0-9]", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Container
        MediaHandler.To(r => r.Container).FromRegex(new Regex(@"\b(MKV|AVI|MP4)\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),

        // Source
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\b(?:HD-?)?CAM(?:RIP)?\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\b(?:HD-?)?T(?:ELE)?S(?:YNC)?\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), value: "telesync"),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\bHD-?Rip\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\bBRRip\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\bBDRip\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\bDVDRip\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\bDVD(?:R[0-9])?\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), value: "dvd"),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\bDVDscr\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\b(?:HD-?)?TVRip\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\bTC\b", RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\bPPVRip\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\bR5\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\bVHSSCR\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\bBluray\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\b(?:PPV )?WEB-?DL\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\bWEB-?Rip\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\b(?:DL|WEB|BD|BR)MUX\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"\b(DivX|XviD)\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Source).FromRegex(new Regex(@"(?:PPV\.)?HDTV", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),

        // Codec
        MediaHandler.To(r => r.Codec).FromRegex(new Regex(@"dvix|mpeg2|divx|xvid|[xh][-. ]?26[45]|avc|hevc", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Codec).FromFunc((value, destination, result) => {
            if (result.Codec != null)
            {
                var property = destination.GetPropertyInfo();
                return new MediaHandler(Regex.Replace(result.Codec, @"[ .-]", ""), int.MaxValue, property);
            }

            return MediaHandler.NoResult();
        }),

        // Audio
        MediaHandler.To(r => r.Audio).FromRegex(new Regex(@"MD|MP3|mp3|FLAC|Atmos|DTS(?:-HD)?|TrueHD", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Audio).FromRegex(new Regex(@"Dual[- ]Audio", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandler.To(r => r.Audio).FromRegex(new Regex(@"AC-?3(?:.5.1)?", RegexOptions.IgnoreCase | RegexOptions.Singleline), value: "ac3"),
        MediaHandler.To(r => r.Audio).FromRegex(new Regex(@"DD5[. ]?1", RegexOptions.IgnoreCase | RegexOptions.Singleline), value: "dd5.1"),
        MediaHandler.To(r => r.Audio).FromRegex(new Regex(@"AAC(?:[. ]?2[. ]0)?", RegexOptions.IgnoreCase | RegexOptions.Singleline), value: "aac"),

        // Group
        MediaHandler.To(r => r.Group).FromRegex(new Regex(@"- ?([^-. ]+)$")),

        // Season
        MediaHandler.To(r => r.Season).FromRegex(new Regex(@"S([0-9]{1,2}) ?E[0-9]{1,2}", RegexOptions.IgnoreCase | RegexOptions.Singleline)),
        MediaHandler.To(r => r.Season).FromRegex(new Regex(@"([0-9]{1,2})x[0-9]{1,2}")),
        MediaHandler.To(r => r.Season).FromRegex(new Regex(@"(?:Saison|Season)[. _-]?([0-9]{1,2})", RegexOptions.IgnoreCase)),

        // Episode
        MediaHandler.To(r => r.Episode).FromRegex(new Regex(@"S[0-9]{1,2} ?E([0-9]{1,2})", RegexOptions.IgnoreCase)),
        MediaHandler.To(r => r.Episode).FromRegex(new Regex(@"[0-9]{1,2}x([0-9]{1,2})")),
        MediaHandler.To(r => r.Episode).FromRegex(new Regex(@"[ée]p(?:isode)?[. _-]?([0-9]{1,3})", RegexOptions.IgnoreCase)),

        // Language
        MediaHandler.To(r => r.Language).FromRegex(new Regex(@"\bRUS\b", RegexOptions.IgnoreCase), lowercase: true),
        MediaHandler.To(r => r.Language).FromRegex(new Regex(@"\bNL\b"), lowercase: true),
        MediaHandler.To(r => r.Language).FromRegex(new Regex(@"\bFLEMISH\b"), lowercase: true),
        MediaHandler.To(r => r.Language).FromRegex(new Regex(@"\bGERMAN\b"), lowercase: true),
        MediaHandler.To(r => r.Language).FromRegex(new Regex(@"\bDUBBED\b"), lowercase: true),
        MediaHandler.To(r => r.Language).FromRegex(new Regex(@"\b(ITA(?:LIAN)?|iTALiAN)\b"), value: "ita"),
        MediaHandler.To(r => r.Language).FromRegex(new Regex(@"\bFR(?:ENCH)?\b"), lowercase: true),
        MediaHandler.To(r => r.Language).FromRegex(new Regex(@"\bTruefrench|VF(?:[FI])\b", RegexOptions.IgnoreCase), lowercase: true),
        MediaHandler.To(r => r.Language).FromRegex(new Regex(@"\bVOST(?:(?:F(?:R)?)|A)?|SUBFRENCH\b", RegexOptions.IgnoreCase), lowercase: true),
        MediaHandler.To(r => r.Language).FromRegex(new Regex(@"\bMULTi(?:Lang|-VF2)?\b", RegexOptions.IgnoreCase), lowercase: true),
    });
        #endregion
    }

    public class MediaParserResult
    {
        public string Title { get; set; } = string.Empty;
        public int? Year { get; set; }
        public string? Resolution { get; set; }
        public bool Extended { get; set; }
        public bool Convert { get; set; }
        public bool Hardcoded { get; set; }
        public bool Proper { get; set; }
        public bool Repack { get; set; }
        public bool Retail { get; set; }
        public bool Remastered { get; set; }
        public bool Unrated { get; set; }
        public string? Region { get; set; }
        public string? Container { get; set; }
        public string? Source { get; set; }
        public string? Codec { get; set; }
        public string? Audio { get; set; }
        public string? Group { get; set; }
        public int? Season { get; set; }
        public int? Episode { get; set; }
        public string? Language { get; set; }
        public MediaType Type
            => Episode != null ? MediaType.Tv : MediaType.Movie;
    }
}