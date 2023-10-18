﻿
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using netcoreAPI.Dal;
using netcoreAPI.Helper;
using netcoreAPI.Hubs;
using netcoreAPI.Models;
using netcoreAPI.Options;
using netcoreAPI.Repository;
using netcoreAPI.Services;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace netcoreAPI.Extensions
{
    public static class StartUp
    {

        public static void ConfigureWebBuilder(this WebApplicationBuilder builder)
        {
            //Add Cors Policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("corsPolicy",
                    policy => policy.WithOrigins("https://localhost:7148")
                                    .AllowAnyHeader());
            });

           
            builder.Services.AddSwaggerGen();

            //Add Versioning Support
            /****************************************************************************************/
            builder.Services.AddApiVersioning(
                    options =>
                    {
                        // reporting api versions will return the headers
                        // "api-supported-versions" and "api-deprecated-versions"
                        options.ReportApiVersions = true;
                        options.DefaultApiVersion = new ApiVersion(2,0);
                        options.AssumeDefaultVersionWhenUnspecified = true;
                        options.Policies.Sunset(1)
                                        .Effective(DateTimeOffset.Now.AddDays(60));
                    })
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
            /****************************************************************************************/

            //add serilog for write log to file
            builder.Logging.AddSerilog(new LoggerConfiguration().WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day).CreateLogger());

            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.ConfigureServices(builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>());
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services, JwtSettings jwtSettings)
        {
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
            /*******************************************************************************/
            services.AddAuthorization();

            //add JWT as scheme for authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = jwtSettings.Audience,
                    ValidIssuer = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });
            /*******************************************************************************/

            //Add injections
            /*******************************************************************************/
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddDbContext<AppDbContext>();

            services.TryAddTransient<CarRepository, CarRepository>();
            services.TryAddTransient<UserRepository, UserRepository>();
            services.TryAddTransient<BrandRepository, BrandRepository>();

            services.TryAddTransient<ISignalRHub, SignalRHub>();
            services.TryAddTransient<IUserService, UserService>();
            services.TryAddTransient<ICarService, CarService>();

            services.AddScoped<IJwtService, JwtService>();
            /*******************************************************************************/

            return services;
        }

        public static IApplicationBuilder ConfigureAppBuilder(this WebApplication app)
        {
            app.UseCors("corsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
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
            }

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
                return Results.Ok(new ApiInfoModel("Canalini, Bruno", "v2.0"));
            })
            .Produces<ApiInfoModel>(StatusCodes.Status200OK)
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
