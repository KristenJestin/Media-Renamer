﻿using MediaRenamer.Common.Extensions;
using System;
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

        public Func<string, MediaParserResult, MediaHandler> FromRegex(Regex regex, bool lowercase = false, string? value = null)
            => (name, _) =>
            {
                var match = regex.Match(name);
                if (match.Success)
                {
                    var property = Destination.GetPropertyInfo();

                    if (!string.IsNullOrWhiteSpace(value))
                        return new MediaHandler(value, match.Index, property);

                    var rawMatch = match.Value?.Trim() ?? string.Empty;
                    var cleanMatch = match.Groups[1]?.Value?.Trim();
                    var matchValue = cleanMatch ?? rawMatch;

                    if (lowercase)
                        matchValue = matchValue.ToLowerInvariant();

                    return new MediaHandler(matchValue, match.Index, property);
                }
                return MediaHandler.NoResult();
            };

        public Func<string, MediaParserResult, MediaHandler> FromFunc(Func<string, Expression<Func<MediaParserResult, TProperty>>, MediaParserResult, MediaHandler> func)
            => (name, result) => func(name, Destination, result);
    }

    public class MediaHandler
    {
        public string? Name { get; }
        public int? Index { get; }
        public string Value { get; }
        public PropertyInfo? Property { get; }

        public MediaHandler(string value, int? index, PropertyInfo? property)
        {
            Value = value;
            Index = index;
            Name = property?.Name;
            Property = property;
        }

        public static MediaHandler NoResult()
            => new MediaHandler(string.Empty, null, null);

        public static MediaHandlerBuilder<T> To<T>(Expression<Func<MediaParserResult, T>> destination)
            => new MediaHandlerBuilder<T>(destination);
    }
}