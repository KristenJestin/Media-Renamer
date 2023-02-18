using MediaRenamer.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MediaRenamer.Media.Models
{

    public class MediaHandlerBuilder<TProperty>
    {
        public Expression<Func<MediaParserResult, TProperty>> Destination { get; }

        public MediaHandlerBuilder(Expression<Func<MediaParserResult, TProperty>> destination)
        {
            Destination = destination;
        }

        public Func<string, MediaParserResult, IEnumerable<MediaHandlerResult>> FromRegex(Regex regex, bool lowercase = false, string? value = null, bool reduceEndOfTitleOffset = true, bool replaceIfAlreadySet = false)
            => (name, _) =>
            {
                var results = new List<MediaHandlerResult>();

                var matches = regex.Matches(name);
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        var property = Destination.GetPropertyInfo();
                        var rawValue = match.Value?.Trim() ?? string.Empty;

                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            results.Add(new MediaHandlerResult(value, match.Index, property, rawValue, reduceEndOfTitleOffset: reduceEndOfTitleOffset, replaceIfAlreadySet: replaceIfAlreadySet));
                            continue;
                        }


                        var cleanMatch = match.Groups[1]?.Value?.Trim();
                        var matchValue = cleanMatch ?? rawValue;

                        if (lowercase)
                            matchValue = matchValue.ToLowerInvariant();

                        results.Add(new MediaHandlerResult(matchValue, match.Index, property, rawValue, reduceEndOfTitleOffset: reduceEndOfTitleOffset, replaceIfAlreadySet: replaceIfAlreadySet));
                    }

                }

                return results;
            };

        public Func<string, MediaParserResult, IEnumerable<MediaHandlerResult>> FromFunc(Func<string, Expression<Func<MediaParserResult, TProperty>>, MediaParserResult, IEnumerable<MediaHandlerResult>> func)
            => (name, result) => func(name, Destination, result);
    }

    public class MediaHandlerResult
    {
        public string? Name { get; }
        public int? Index { get; }
        public string Value { get; }
        public PropertyInfo? Property { get; }
        public string RawValue { get; }
        public bool ReduceEndOfTitleOffset { get; }
        public bool ReplaceIfAlreadySet { get; }

        public MediaHandlerResult(string value, int? index, PropertyInfo? property, string rawValue, bool reduceEndOfTitleOffset = true, bool replaceIfAlreadySet = false)
        {
            Value = value;
            Index = index;
            Name = property?.Name;
            Property = property;
            RawValue = rawValue;
            ReduceEndOfTitleOffset = reduceEndOfTitleOffset;
            ReplaceIfAlreadySet = replaceIfAlreadySet;
        }

        public static MediaHandlerBuilder<T> To<T>(Expression<Func<MediaParserResult, T>> destination)
            => new MediaHandlerBuilder<T>(destination);
    }
}