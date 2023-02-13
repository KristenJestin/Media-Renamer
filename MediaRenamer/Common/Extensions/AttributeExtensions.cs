using System.ComponentModel;
using System.Globalization;

namespace MediaRenamer.Common.Extensions;

public static class AttributeExtensions
{
    public static string? GetDescription<T>(this T e) where T : IConvertible
    {
        if (e is Enum)
        {
            var type = e.GetType();
            var values = System.Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == e.ToInt32(CultureInfo.InvariantCulture))
                {
                    var memInfo = type.GetMember(type.GetEnumName(val));
                    var descriptionAttribute = memInfo[0]
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() as DescriptionAttribute;

                    if (descriptionAttribute != null)
                        return descriptionAttribute.Description;
                }
            }
        }

        return null; // could also return string.Empty
    }
}
