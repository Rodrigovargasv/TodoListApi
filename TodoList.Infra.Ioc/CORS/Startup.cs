
using Microsoft.Extensions.DependencyInjection;

namespace TodoList.Infra.Ioc.CORS
{
    internal static class Startup
    {
        internal static IServiceCollection AddServiceCors(this IServiceCollection services)
        {
            // Adicionar serviço de política de acesso a Api.
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder
                     .AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();



                });
            });

            return services;
        }
    }
}
