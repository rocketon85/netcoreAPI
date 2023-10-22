using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using netcoreAPI.Dal;
using netcoreAPI.Helper;
using netcoreAPI.Hubs;
using netcoreAPI.Middlewares;
using netcoreAPI.Models;
using netcoreAPI.Options;
using netcoreAPI.Repository;
using netcoreAPI.Services;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace netcoreAPI.Extensions
{
    public static class StartUp 
    {
        //public static void ConfigureProduction()
        //{
        //    var builder = WebApplication.CreateBuilder();

        //    // Add services to the container.
        //    builder.Services.AddControllers();
        //    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //    builder.Services.AddEndpointsApiExplorer();

        //    //using extension for cammon settings
        //    builder.ConfigureWebBuilder();
        //    builder.Services.ConfigureDefaultServices(builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>());

        //    var app = builder.Build();

        //    app.UseHttpsRedirection();
        //    app.MapControllers();

        //    //using extension for cammon settings
        //    app.ConfigureAppBuilder();
        //    app.ConfigureMinimal();

        //    app.Run();

        //}

        public static void ConfigureWebBuilder(this WebApplicationBuilder builder)
        {
            //add serilog for write log to file
            builder.Logging.AddSerilog(new LoggerConfiguration().WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day).CreateLogger());

            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        }

        public static IServiceCollection ConfigureDefaultServices(this IServiceCollection services, JwtSettings? jwtSettings)
        {
            //Add Localization Support
            services.AddConfigureLocalization();

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
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddTransient<IConfigureOptions<ApiVersioningOptions>, ConfigureApiVersioningOptions>();

            services.TryAddTransient<ISignalRHub, SignalRHub>();

            services.AddDbContext<AppDbContext>();

            services.TryAddTransient<CarRepository, CarRepository>();
            services.TryAddTransient<UserRepository, UserRepository>();
            services.TryAddTransient<BrandRepository, BrandRepository>();

            
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

            var localizeOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            if(localizeOptions != null)  app.UseRequestLocalization(localizeOptions.Value);

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
                return await Task.FromResult(Results.Ok(new ApiInfoModel("Canalini, Bruno", "v2.0")));
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
