using System.Text.RegularExpressions;

namespace MediaRenamer.Common.Extensions;

public static class RegexExtensions
{
    public static IEnumerable<Group> GetSuccessGroups(this Match match)
        => match.Groups.Values.Where(g => g.Success);
}