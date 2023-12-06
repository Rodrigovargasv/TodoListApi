
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Infra.Data.Context;

namespace TodoList.Infra.Ioc.DpContext
{
    internal static class Startup
    {
        internal static IServiceCollection AddServiceDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Adiciona serviço do banco de dados Postgresql
            services.AddDbContext<ApplicationDbContext>(option =>
            option.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            bulder => bulder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Altera o formato de data e hora do banco de dados PostgreSql
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Adicionar serviço de Hangfire para armazenamento das tarefas no Memory Storage
            services.AddHangfire(x => x.UseMemoryStorage());
            services.AddHangfireServer();

            // Faz o armazenamento de dados em cache.
            services.AddMemoryCache();


            return services;
        }
    }
}
