

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Infra.Data.Context;

namespace TodoList.Infra.Ioc
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
            option.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            bulder => bulder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            return services;
        }

    }
}
