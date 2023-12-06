
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.ExceptionMiddleware;

namespace TodoList.Infra.Ioc.Execption
{
    internal static class Startup
    {

        internal static IServiceCollection AddExpectionDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ExceptionMiddleware>();
            return services;
        }

        internal static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
