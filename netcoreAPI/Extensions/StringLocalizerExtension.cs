using Microsoft.Extensions.Localization;

namespace netcoreAPI.Extensions
{
    public static class StringLocalizerExtension
    {
        public static string GetValue<T>(this IStringLocalizer localizer, T element, string field)
        {
            if (!localizer.GetAllStrings().Any(p => p.Name == field) && element != null && element.GetType().GetFields().Any(p => p.Name == field)) return element.GetType().GetField(field).GetValue(element).ToString();
            return localizer[field]?.Value == field ? element?.GetType().GetField(field).GetValue(element).ToString() : localizer[field]?.Value;
        }
    }
}
