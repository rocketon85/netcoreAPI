using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using netcoreAPI.Extensions;
using netcoreAPI.Structures;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace netcoreAPI.Options
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class SwaggerOption : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly IStringLocalizer<SwaggerOption> _localizer;
        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerOption"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public SwaggerOption(IApiVersionDescriptionProvider provider, IStringLocalizer<SwaggerOption> localizer)
        {
            _provider = provider;
            _localizer = localizer;
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            options.AddSecurityDefinition(EnviromentSettings.SecuritySchemeAuthentication, new OpenApiSecurityScheme()
            {
                Name = EnviromentSettings.SecuritySchemeName,
                Type = SecuritySchemeType.ApiKey,
                Scheme = EnviromentSettings.SecuritySchemeAuthentication,
                BearerFormat = EnviromentSettings.SecuritySchemeBearerFormat,
                In = ParameterLocation.Header,
                Description = _localizer.GetValue<EnviromentLanguage>(new EnviromentLanguage(), "SecuritySchemeDescription"),
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = EnviromentSettings.SecuritySchemeAuthentication
                            }
                        },
                        new string[] {}
                    }
                });

            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var text = new StringBuilder(_localizer.GetValue<EnviromentLanguage>(new EnviromentLanguage(), "AppDescription"));
            var info = new OpenApiInfo()
            {
                Title = _localizer.GetValue<EnviromentLanguage>(new EnviromentLanguage(), "AppName"),
                Version = description.ApiVersion.ToString(),
                Contact = new OpenApiContact() { Name = EnviromentSettings.AppContactName, Email = EnviromentSettings.AppContactMail },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };

            if (description.IsDeprecated)
            {
                text.Append($"</br> {_localizer.GetValue<EnviromentLanguage>(new EnviromentLanguage(), "AppVersionDeprecated")}");
            }

            if (description.SunsetPolicy is SunsetPolicy policy)
            {
                if (policy.Date is DateTimeOffset when)
                {
                    text.Append($"</br> {_localizer.GetValue<EnviromentLanguage>(new EnviromentLanguage(), "AppVersionDeprecatedSunsetOn")} {when.Date.ToShortDateString()}.");
                }

                if (policy.HasLinks)
                {
                    text.AppendLine();

                    for (var i = 0; i < policy.Links.Count; i++)
                    {
                        var link = policy.Links[i];

                        if (link.Type == "text/html")
                        {
                            text.AppendLine();

                            if (link.Title.HasValue)
                            {
                                text.Append(link.Title.Value).Append(": ");
                            }

                            text.Append(link.LinkTarget.OriginalString);
                        }
                    }
                }
            }

            info.Description = text.ToString();

            return info;
        }
    }
}
