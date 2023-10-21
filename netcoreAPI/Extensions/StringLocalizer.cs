using Microsoft.Extensions.Localization;

namespace netcoreAPI.Extensions
{
    public static class StringLocalizer
    {
        public static string GetValue<T>(this IStringLocalizer localizer, T element, string field)
        {
            return localizer[field].Value == field ? element.GetType().GetField(field).GetValue(element).ToString() : localizer[field].Value;
        }
    }
}
