using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;
using netcoreAPI.Helper;
using netcoreAPI.Options;
using System.Globalization;
using System.Text;

namespace netcoreAPI.Extensions
{
    public static class LocalizationExtension
    {
        public static IServiceCollection AddConfigureLocalization(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("es-ES")
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                    {
                        var userLangs = context.Request.Headers["Accept-Language"].ToString();
                        var firstLang = userLangs.Split(',').FirstOrDefault();
                        var defaultLang = string.IsNullOrEmpty(firstLang) ? "en-US" : firstLang;
                        return Task.FromResult(new ProviderCultureResult(defaultLang, defaultLang));
                    }));
                });

                //if we want to add support localization in route
                //services.Configure<RouteOptions>(options =>
                //{
                //    options.ConstraintMap.Add("culture", typeof(LanguageRouteConstraint));
                //});

            return services;
        }
    }
}
