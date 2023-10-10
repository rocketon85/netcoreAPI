﻿
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

namespace netcoreAPI.Extensions
{
    public static class StartUp
    {

        public static void AddDefualtSettings(this WebApplicationBuilder builder)
        {
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
            builder.Services.AddDefualtSettings(builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>());
        }

        public static IServiceCollection AddDefualtSettings(this IServiceCollection services, JwtSettings jwtSettings)
        {

            services.AddDbContext<AppDbContext>();

            services.TryAddTransient<CarRepository, CarRepository>();
            services.TryAddTransient<IUserService, UserService>();
            services.TryAddTransient<UserRepository, UserRepository>();
            services.TryAddTransient<BrandRepository, BrandRepository>();

            services.AddScoped<IJwtService, JwtService>();

            services.AddAuthorization();
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

        public static IApplicationBuilder UseDefualtSettings(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            return app;
        }
    }
}
