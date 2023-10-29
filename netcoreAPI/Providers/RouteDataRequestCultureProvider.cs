using Microsoft.AspNetCore.Localization;

namespace netcoreAPI.Providers
{
    public class RouteDataRequestCultureProvider : RequestCultureProvider
    {
        public int IndexOfCulture;
        public int IndexofUICulture;

        public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
                return Task.FromResult<ProviderCultureResult?>(null);

            string culture = string.Empty;
            string uiCulture = string.Empty;

            if (httpContext.Request.Path.HasValue && httpContext.Request.Path.Value.Split('/').Length > IndexOfCulture)
            {
                culture = uiCulture = httpContext.Request.Path.Value.Split('/')[IndexOfCulture].ToString();
            }


            var providerResultCulture = new ProviderCultureResult(culture, uiCulture);

            return Task.FromResult<ProviderCultureResult?>(providerResultCulture);
        }
    }
}
