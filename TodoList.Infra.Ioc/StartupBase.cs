
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Infra.Ioc.DpContext;
using TodoList.Infra.Ioc.Services;
using TodoList.Infra.Ioc.Swagger;
using TodoList.Infra.Ioc.JWT;
using TodoList.Infra.Ioc.CORS;

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

            //Services de autenticação e autorização de usuário
            services.AddServiceJwtAuthenticationAndAutorization(configuration);

            // Servicço de politicas de acesso api CORS
            services.AddServiceCors();


            return services;
        }
    }
}
