using System.ComponentModel;
using System.Reflection;

namespace ApiWorkshop.Application.Domain.Extensions;
public static class Enums
{
    private static string GetEnumDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString())!;

        DescriptionAttribute[]? attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Any())
        {
            return attributes.First().Description;
        }

        return value.ToString();
    }
    public static string ToDescription<TEnum>(this TEnum EnumValue) where TEnum : struct
    {
        return GetEnumDescription((Enum)(object)((TEnum)EnumValue));
    }
}