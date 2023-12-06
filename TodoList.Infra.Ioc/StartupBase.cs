
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Infra.Ioc.DpContext;
using TodoList.Infra.Ioc.Services;
using TodoList.Infra.Ioc.Swagger;
using TodoList.Infra.Ioc.JWT;
using TodoList.Infra.Ioc.CORS;
using TodoList.Infra.Ioc.Execption;
using Microsoft.AspNetCore.Builder;

namespace TodoList.Infra.Ioc
{
    public static class StartupBase
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Serviço de banco de dados e armazenamento.
            services.AddServiceDbContext(configuration);

            services.AddServicesBase(configuration);

            // Serviço para halitar autenticação com swagger
            services.AddServiceSwagger();

            // Service de autenticação e autorização de usuário
            services.AddServiceJwtAuthenticationAndAutorization(configuration);

            // Serviço de politicas de acesso api CORS
            services.AddServiceCors();

            // Serviço de tratamento de execptions
            services.AddExpectionDependencyInjection();


            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config)
        {
            builder
                .UseExceptionMiddleware();
            return builder;
        }
    }
}
