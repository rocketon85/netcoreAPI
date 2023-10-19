using Asp.Versioning;
using netcoreAPI.Options;

namespace netcoreAPI.Extensions
{
    public static class ApiVersioningExtension
    {
        public static IApiVersioningBuilder AddConfigureApiVersioning(this IServiceCollection services)
        {
            //services.Configure<ApiVersioningOptions>(new ConfigureApiVersioningOptions().Configure);
            return services.AddApiVersioning()
                .AddMvc()
                .AddApiExplorer(
                    options =>
                    {
                        // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                        // note: the specified format code will format the version as "'v'major[.minor][-status]"
                        options.GroupNameFormat = "'v'VVV";

                        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                        // can also be used to control the format of the API version in route templates
                        options.SubstituteApiVersionInUrl = true;
                    });
        }
    }
}
