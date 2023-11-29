

using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TodoList.Infra.Ioc.Swagger
{
    internal static class Startup
    {
        internal static IServiceCollection AddServiceSwagger(this IServiceCollection services)
        {
         
            // Adicionado o serviço de autenticação com JWT bearer.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoListApi", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira 'Bearer' e um token JWT válido."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] { }
                    }
                });
            });


            return services;
        }
    }
}
