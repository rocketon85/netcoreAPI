using Asp.Versioning;
using Microsoft.Extensions.Options;

namespace netcoreAPI.Options
{
    public class ConfigureApiVersioningOptions : IConfigureOptions<ApiVersioningOptions>
    {
        public void Configure(ApiVersioningOptions options)
        {
                // reporting api versions will return the headers
                // "api-supported-versions" and "api-deprecated-versions"
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(2, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.Policies.Sunset(1).Effective(DateTimeOffset.Now.AddDays(60));
                // allow multiple locations to request an api version
                //options.ApiVersionReader = ApiVersionReader.Combine(
                //    new QueryStringApiVersionReader(),
                //    new HeaderApiVersionReader("api-version", "x-ms-version"));
        }

    }
}
