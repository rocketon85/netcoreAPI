﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using netcoreAPI.Options;
using System.Text;

namespace netcoreAPI.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddConfigureAuthentication(this IServiceCollection services, JwtOption? jwtSettings)
        {
            services.AddAuthorization();

            if (jwtSettings == null) return services;
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
    }
}
