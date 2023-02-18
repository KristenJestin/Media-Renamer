using MediaRenamer.Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MediaRenamer.Media.Models
{
    public class MediaParser
    {
        public IEnumerable<Func<string, MediaParserResult, IEnumerable<MediaHandlerResult>>> Handlers { get; }


        public MediaParser(IEnumerable<Func<string, MediaParserResult, IEnumerable<MediaHandlerResult>>> handlers)
        {
            Handlers = handlers;
        }

        public MediaParserResult Parse(string value)
        {
            var result = new MediaParserResult();
            var endOfTitle = value.Length;
            var title = value;

            foreach (var handler in Handlers)
            {
                var handlerResult = handler(title, result);
                if (handlerResult != null && handlerResult.Any())
                {
                    foreach (var item in handlerResult)
                    {
                        if (!item.Index.HasValue || item.Property == null)
                            continue;

                        if (item.ReduceEndOfTitleOffset && item.Index < endOfTitle)
                            endOfTitle = item.Index.Value;

                        if (!string.IsNullOrEmpty(item.RawValue))
                            title = title.Replace(item.RawValue, "");

                        var t = Nullable.GetUnderlyingType(item.Property.PropertyType) ?? item.Property.PropertyType;
                        try
                        {
                            if (item.Value != null && item.Property.GetValue(result) == null)
                                item.Property?.SetValue(result, Convert.ChangeType(item.Value, t), null);
                        }
                        catch { }
                    }
                }
            }

            var parentheiseIndex = title.IndexOf('(');
            if (parentheiseIndex != -1 && parentheiseIndex < endOfTitle)
                endOfTitle = parentheiseIndex;

            result.Title = CleanTitle(title[..endOfTitle]);
            return result;
        }


        #region privates
        private string CleanTitle(string title)
        {
            // remove all group
            title = Regex.Replace(title, @"(^[-\. ()]+)|([-\. ]+$)", " ");
            title = Regex.Replace(title, @"[\(\)\/]", " ");
            // keep only alphanumeric
            title = Regex.Replace(title, "[^a-zA-Z0-9-:']", " ");
            // remove double space
            title = Regex.Replace(title, @"\s+", " ");
            title = title.Trim();

            return title;
        }
        #endregion

        #region statics
        public static readonly MediaParser Default = new MediaParser(new[] {
        // Year
        MediaHandlerResult.To(r => r.Year).FromRegex(new Regex(@"(?!^)((?:19[0-9]|20[012])[0-9])", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Website
        MediaHandlerResult.To(r => r.Website).FromRegex(new Regex(@"^(\[ ?([^\]]+?) ?\])", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true, reduceEndOfTitleOffset: false),

        // Resolution
        MediaHandlerResult.To(r => r.Resolution).FromRegex(new Regex(@"([0-9]{3,4}[pi])", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Resolution).FromRegex(new Regex(@"(4k(?:Light)?)", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),

        // Extended
        MediaHandlerResult.To(r => r.Extended).FromRegex(new Regex(@"(EXTENDED(:?.CUT)?)", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Convert
        MediaHandlerResult.To(r => r.Convert).FromRegex(new Regex(@"CONVERT", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Hardcoded
        MediaHandlerResult.To(r => r.Hardcoded).FromRegex(new Regex(@"HC|HARDCODED", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Proper
        MediaHandlerResult.To(r => r.Proper).FromRegex(new Regex(@"(?:REAL.)?PROPER", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Repack
        MediaHandlerResult.To(r => r.Repack).FromRegex(new Regex(@"REPACK|RERIP", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Retail
        MediaHandlerResult.To(r => r.Retail).FromRegex(new Regex(@"\bRetail\b", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Remastered
        MediaHandlerResult.To(r => r.Remastered).FromRegex(new Regex(@"\bRemaster(?:ed)?\b", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Unrated
        MediaHandlerResult.To(r => r.Unrated).FromRegex(new Regex(@"\bunrated|uncensored\b", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Region
        MediaHandlerResult.To(r => r.Region).FromRegex(new Regex(@"R[0-9]", RegexOptions.IgnoreCase | RegexOptions.Singleline)),

        // Container
        MediaHandlerResult.To(r => r.Container).FromRegex(new Regex(@"\b(MKV|AVI|MP4)\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),

        // Size
        MediaHandlerResult.To(r => r.Size).FromRegex(new Regex(@"(\d+(?:\.\d+)?(?:GB|MB))", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),

        // Source
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\b(?:HD-?)?CAM(?:RIP)?\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\b(?:HD-?)?T(?:ELE)?S(?:YNC)?\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), value: "telesync"),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\bHD-?R(?:ip)?\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\bBRRip\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\bBDRip\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\bDVDRip\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\bDVD(?:R[0-9])?\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), value: "dvd"),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\bDVDscr\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\b(?:HD-?)?TVRip\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\bTC\b", RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\bPPVRip\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\bR5\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\bVHSSCR\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\bBluray\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\b(?:PPV )?WEB-?DL\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\bW[EB]B-?Rip\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\b(?:DL|WEB|BD|BR)MUX\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"\b(DivX|XviD)\b", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Source).FromRegex(new Regex(@"(?:PPV\.)?HDTV", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),

        // Codec
        MediaHandlerResult.To(r => r.Codec).FromRegex(new Regex(@"dvix|mpeg2|divx|xvid|[xh][-. ]?26[45]|avc|hevc|10bit", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Codec).FromFunc((value, destination, result) => {
            if (result.Codec != null)
            {
                var property = destination.GetPropertyInfo();
                var data = Regex.Replace(result.Codec, @"[ .-]", "");
                return new []{ new MediaHandlerResult(data, int.MaxValue, property,data) };
            }

            return Enumerable.Empty<MediaHandlerResult>();
        }),

        // Audio
        MediaHandlerResult.To(r => r.Audio).FromRegex(new Regex(@"MD|MP3|mp3|FLAC|Atmos|LiNE|DTS(?:-HD)?|TrueHD", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Audio).FromRegex(new Regex(@"Dual[- ]Audio", RegexOptions.IgnoreCase | RegexOptions.Singleline), lowercase: true),
        MediaHandlerResult.To(r => r.Audio).FromRegex(new Regex(@"AC-?3(?:.5.1)?", RegexOptions.IgnoreCase | RegexOptions.Singleline), value: "ac3"),
        MediaHandlerResult.To(r => r.Audio).FromRegex(new Regex(@"DD5[. ]?1", RegexOptions.IgnoreCase | RegexOptions.Singleline), value: "dd5.1"),
        MediaHandlerResult.To(r => r.Audio).FromRegex(new Regex(@"AAC(?:(?:[. ]?2[. ]0)|(?:.?7.1))?", RegexOptions.IgnoreCase | RegexOptions.Singleline), value: "aac"),

        // Group
        //MediaHandlerResult.To(r => r.Group).FromRegex(new Regex(@"(- ?([^-]+(?:-={[^-]+-?$)?))$")),

        // Season
        MediaHandlerResult.To(r => r.Season).FromRegex(new Regex(@"S([0-9]{1,2}) ?E[0-9]{1,2}", RegexOptions.IgnoreCase | RegexOptions.Singleline)),
        MediaHandlerResult.To(r => r.Season).FromRegex(new Regex(@"([0-9]{1,2})x[0-9]{1,2}")),
        MediaHandlerResult.To(r => r.Season).FromRegex(new Regex(@"(?:Saison|Season)[. _-]?([0-9]{1,2})", RegexOptions.IgnoreCase)),

        // Episode
        MediaHandlerResult.To(r => r.Episode).FromRegex(new Regex(@"S[0-9]{1,2} ?E([0-9]{1,2})", RegexOptions.IgnoreCase)),
        MediaHandlerResult.To(r => r.Episode).FromRegex(new Regex(@"[0-9]{1,2}x([0-9]{1,2})")),
        MediaHandlerResult.To(r => r.Episode).FromRegex(new Regex(@"[ée]p(?:isode)?[. _-]?([0-9]{1,3})", RegexOptions.IgnoreCase)),

        // Language
        MediaHandlerResult.To(r => r.Language).FromRegex(new Regex(@"\bRUS\b", RegexOptions.IgnoreCase), lowercase: true),
        MediaHandlerResult.To(r => r.Language).FromRegex(new Regex(@"\bNL\b"), lowercase: true),
        MediaHandlerResult.To(r => r.Language).FromRegex(new Regex(@"\bFLEMISH\b"), lowercase: true),
        MediaHandlerResult.To(r => r.Language).FromRegex(new Regex(@"\bGERMAN\b"), lowercase: true),
        MediaHandlerResult.To(r => r.Language).FromRegex(new Regex(@"\bDUBBED\b"), lowercase: true),
        MediaHandlerResult.To(r => r.Language).FromRegex(new Regex(@"\b(ITA(?:LIAN)?|iTALiAN)\b"), value: "ita"),
        MediaHandlerResult.To(r => r.Language).FromRegex(new Regex(@"\bFR(?:ENCH)?\b"), lowercase: true),
        MediaHandlerResult.To(r => r.Language).FromRegex(new Regex(@"\bTruefrench|VF(?:[FI])\b", RegexOptions.IgnoreCase), lowercase: true),
        MediaHandlerResult.To(r => r.Language).FromRegex(new Regex(@"\bVOST(?:(?:F(?:R)?)|A)?|SUBFRENCH\b", RegexOptions.IgnoreCase), lowercase: true),
        MediaHandlerResult.To(r => r.Language).FromRegex(new Regex(@"\bMULTi(?:Lang|-VF2)?\b", RegexOptions.IgnoreCase), lowercase: true),
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
        public string? Website { get; set; }
        public string? Size { get; set; }
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