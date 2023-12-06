
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.Design;
using System.Text;

namespace TodoList.Infra.Ioc.JWT
{
    internal static class Startup
    {
        internal static IServiceCollection AddServiceJwtAuthenticationAndAutorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(x =>
               {
                   x.RequireHttpsMetadata = false;
                   x.SaveToken = true;


                   x.TokenValidationParameters = new TokenValidationParameters
                   {
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                       //ValidateIssuerSigningKey = true,
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true
                   };
               });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("admin", p => p.RequireRole("admin"));
                option.AddPolicy("commonUser", p => p.RequireRole("commonUser"));

            });

            return services;

        }
    }
}
