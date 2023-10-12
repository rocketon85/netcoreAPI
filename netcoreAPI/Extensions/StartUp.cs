
using netcoreAPI.Dal;
using netcoreAPI.Domain;
using netcoreAPI.Helper;
using netcoreAPI.Repository;
using netcoreAPI.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using netcoreAPI.Models;

namespace netcoreAPI.Extensions
{
    public static class StartUp
    {

        public static void ConfigureWebBuilder(this WebApplicationBuilder builder)
        {
            //add cors policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("corsPolicy",
                    policy => policy.WithOrigins("https://localhost:7148")
                                    .AllowAnyHeader());
            });

            //add compatibility for Authorization token
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            //add serilog for write log to file
            builder.Logging.AddSerilog(new LoggerConfiguration().WriteTo.File("yyyy-MM-dd.txt").CreateLogger());

            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.ConfigureServices(builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>());
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services, JwtSettings jwtSettings)
        {

            services.AddDbContext<AppDbContext>();

            services.TryAddTransient<CarRepository, CarRepository>();
            services.TryAddTransient<IUserService, UserService>();
            services.TryAddTransient<UserRepository, UserRepository>();
            services.TryAddTransient<BrandRepository, BrandRepository>();

            services.AddScoped<IJwtService, JwtService>();

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

            return services;
        }

        public static IApplicationBuilder ConfigureAppBuilder(this IApplicationBuilder app)
        {
            app.UseCors("corsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            return app;
        }

        /// <summary>
        /// Add configuration for Minimal API example.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder ConfigureMinimal(this WebApplication app)
        {
            app.MapGet("/api/info", async () =>
            {
                return Results.Ok( new ApiInfoModel ("Canalini, Bruno", "v2.0"));
            })
            .Produces<ApiInfoModel>(StatusCodes.Status200OK)
            .WithOpenApi(operation => new(operation)
            {
                Summary= "API Info",
                Description = "Minimal API endpoint"
            });

            return app;
        }
    }
}
