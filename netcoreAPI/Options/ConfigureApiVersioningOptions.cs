using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

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
        }

    }
}
