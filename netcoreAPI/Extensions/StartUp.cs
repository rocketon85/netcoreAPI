﻿using Asp.Versioning;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using netcoreAPI.Context;
using netcoreAPI.Contracts.Models.Responses;
using netcoreAPI.Helper;
using netcoreAPI.Hubs;
using netcoreAPI.Middlewares;
using netcoreAPI.Options;
using netcoreAPI.Repositories;
using netcoreAPI.Services;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace netcoreAPI.Extensions
{
    public static class StartUp
    {
        public static void ConfigureWebBuilder(this WebApplicationBuilder builder)
        {
            //add serilog for write log to file
            builder.Logging.AddSerilog(new LoggerConfiguration().WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day).CreateLogger());

            builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.Configure<SecurityOption>(builder.Configuration.GetSection("SecuritySettings"));
            builder.Services.Configure<EnviromentOption>(builder.Configuration.GetSection("EnviromentSettings"));
            builder.Services.Configure<AzureOption>(builder.Configuration.GetSection("AzureSettings"));
        }

        public static IServiceCollection ConfigureDefaultServices(this IServiceCollection services, JwtOption? jwtSettings, EnviromentOption? envSettings)
        {

            services.AddControllers()
            .AddOData(options => options.EnableQueryFeatures(null));

            //Add Localization Support
            services.AddConfigureLocalization(envSettings);
            services.AddExceptionHandler<Middlewares.ExceptionHandlerMiddleware>();

            //Add Cors Policy
            services.AddCors(options =>
            {
                options.AddPolicy("corsPolicy",
                    policy => policy.WithOrigins("https://localhost:7148")
                                    .AllowAnyHeader());

            });


            services.AddSwaggerGen();
            services.AddConfigureApiVersioning();

            //Add SignalR Support
            services.AddSignalR();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            //Add AutoMapper Support
            services.AddAutoMapper(typeof(StartUp));

            //Add Authorization
            services.AddConfigureAuthentication(jwtSettings);


            //Add injections
            /*******************************************************************************/
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOption>();
            services.AddTransient<IConfigureOptions<ApiVersioningOptions>, ApiVersioningOption>();
            services.AddSingleton<IAzureKeyVaultService, AzureKeyVaultService>();
            services.AddSingleton<IAzureFuncService, AzureFuncService>();
            services.AddSingleton<IEncryptorHelper, EncryptorHelper>();

            services.TryAddTransient<ISignalRHub, SignalRHub>();

            services.AddDbContext<AppDbContext>();

            services.AddSingleton<IJwtService, JwtService>();

            services.TryAddTransient<IRepositoryWrapper, RepositoryWrapper>();
            /*******************************************************************************/

            return services;
        }

        public static IApplicationBuilder ConfigureAppBuilder(this WebApplication app)
        {
            app.UseCors("corsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseExceptionHandler(o => { }); 

            var localizeOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            if (localizeOptions != null) app.UseRequestLocalization(localizeOptions.Value);

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    var descriptions = app.DescribeApiVersions();

                    // build a swagger endpoint for each discovered API version
                    foreach (var description in descriptions)
                    {
                        var url = $"/swagger/{description.GroupName}/swagger.json";
                        var name = description.GroupName.ToUpperInvariant();
                        options.SwaggerEndpoint(url, name);
                    }
                });
            //}

            return app;
        }

        /// <summary>
        /// Add configuration for Minimal API example.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder ConfigureMinimal(this WebApplication app)
        {
            app.UseResponseCompression();

            //Add Minimal API Example
            app.MapGet("/api/info", async () =>
            {
                return await Task.FromResult(Results.Ok(new ApiInfoResponse() { Author = @"Canalini, Bruno", Version = @"v2.0" }));
            })
            .Produces<ApiInfoResponse>(StatusCodes.Status200OK)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "API Info",
                Description = "Minimal API endpoint"
            });

            //Add SignalR Hub
            app.MapHub<SignalRHub>("/signalhub");


            return app;
        }
    }
}
