using Asp.Versioning;

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
                        options.GroupNameFormat = "'v'VVV";
                        options.SubstituteApiVersionInUrl = true;
                    })
                 .AddOData()
                 .AddODataApiExplorer(
                    options =>
                    {
                        options.GroupNameFormat = "'v'VVV";
                        options.SubstituteApiVersionInUrl = true;
                    });
        }
    }
}
