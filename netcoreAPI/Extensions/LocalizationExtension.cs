using Microsoft.AspNetCore.Localization;
using netcoreAPI.Options;
using System.Globalization;

namespace netcoreAPI.Extensions
{
    public static class LocalizationExtension
    {
        public static IServiceCollection AddConfigureLocalization(this IServiceCollection services, EnviromentOption? envSettings)
        {
            services.AddLocalization(options => options.ResourcesPath = envSettings.ResourcePath);

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = envSettings?.AvailableCulture.ToList().Select(n =>
                    {
                        return new CultureInfo(n);
                    }).ToList();
                    //var supportedCultures = new List<CultureInfo>
                    //{
                    //    new CultureInfo("en-US"),
                    //    new CultureInfo("es-ES")
                    //};

                    options.DefaultRequestCulture = new RequestCulture(culture: envSettings.DefaultCulture, uiCulture: envSettings.DefaultCulture);
                    options.SupportedCultures = (IList<CultureInfo>?)supportedCultures;
                    options.SupportedUICultures = (IList<CultureInfo>?)supportedCultures;
                    options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                    {
                        var userLangs = context.Request.Headers["Accept-Language"].ToString();
                        var firstLang = userLangs.Split(',').FirstOrDefault();
                        var defaultLang = string.IsNullOrEmpty(firstLang) ? envSettings.DefaultCulture : firstLang;
                        return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(defaultLang, defaultLang));
                    }));
                });

            //if we want to add support localization in route
            //services.Configure<RouteOptions>(options =>
            //{
            //    options.ConstraintMap.Add("culture", typeof(LanguageRouteOption));
            //});

            return services;
        }
    }
}
