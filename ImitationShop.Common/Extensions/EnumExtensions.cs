namespace ImitationShop.Common.Extensions;

public static class EnumExtensions
{
    public static string ToDescription<TEnum>(this TEnum enumValue) where TEnum : struct
    {
        return GetEnumDescription((Enum)(object)enumValue);
    }

    public static string GetEnumDescription(Enum value)
    {
        FieldInfo? fi = value.GetType().GetField(value.ToString());

        if (fi!.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
        {
            return attributes.First().Description;
        }
        return value.ToString();
    }
}
