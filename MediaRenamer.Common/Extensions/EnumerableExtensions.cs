using System.Collections.Generic;

namespace MediaRenamer.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static string Join<T>(this IEnumerable<T> list, string separator = ",")
            => string.Join(separator, list);
    }
}
