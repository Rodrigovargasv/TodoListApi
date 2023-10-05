
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.Interfaces;
using TodoList.Application.Services;
using TodoList.Domain.Interfaces;
using TodoList.Infra.Data.Context;
using TodoList.Infra.Data.Repository;


namespace TodoList.Infra.Ioc
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
            option.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            bulder => bulder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<EmailModel>();
            services.AddScoped<SendEmailService>();
            services.AddScoped<JobService>();

            services.AddScoped<ISendEmail, SendEmailService>();


            return services;
        }


      

    }
}
