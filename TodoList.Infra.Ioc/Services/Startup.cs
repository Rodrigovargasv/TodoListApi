using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.Interfaces;
using TodoList.Application.Services;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Validation;
using TodoList.Infra.Data.Repository;

namespace TodoList.Infra.Ioc.Services
{
    internal static class Startup
    {
        internal static IServiceCollection AddServicesBase(this IServiceCollection services, IConfiguration configuration)
        {



            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISendEmail, SendEmailService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRecoveryPasswordUserRepository, RecoveryPasswordRepository>();
            services.AddScoped<IRecoveryPasswordUserService, RecoveryPassawordUserService>();
            services.AddScoped<IScheduleEmailSedingService, ScheduledEmailSendingService>();


            services.AddTransient<TokenService>();
            services.AddScoped<GenerationCodeRecoveryService>();
            services.AddScoped<JobValidation>();
            services.AddScoped<EmailValidation>();
            services.AddScoped<UserValidation>();
          
            

            // Obter os dados de envio de email do appsettings
            services.Configure<EmailSetting>(configuration.GetSection("EmailSettings"));
            services.AddScoped<EmailSetting>();


            return services;
        }
    }
}
