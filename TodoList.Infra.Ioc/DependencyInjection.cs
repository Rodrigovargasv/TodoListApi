
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoList.Application.Interfaces;
using TodoList.Application.Services;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Infra.Data.Context;
using TodoList.Infra.Data.Repository;
using AutoMapper;
using TodoList.Application.Mappings;
using System.Reflection;

namespace TodoList.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
            option.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            bulder => bulder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Obter os dados de envio de email do appsettings
            services.Configure<EmailSetting>(configuration.GetSection("EmailSettings"));
            services.AddScoped<EmailSetting>();

          

            // habilita sistema agenda envios dos emails
            services.AddHangfire(x => x.UseMemoryStorage());
            services.AddHangfireServer();
            services.AddAutoMapper(typeof(MappingsEntityDTOs));

            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISendEmail, SendEmailService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRecoveryPasswordUserRepository, RecoveryPasswordRepository>();
            services.AddScoped<IRecoveryPasswordUserService, RecoveryPassawordUserService>();


            services.AddScoped<JobService>();
            services.AddScoped<SendEmailService>();
            services.AddScoped<EmailService>();
            services.AddScoped<RecoveryPassawordUserService>();
            services.AddScoped<GenerationCodeRecoveryService>();


            // faz o armazenamento de dados em cache.
            services.AddMemoryCache();


            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);

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
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });


    

            return services;
        }
    }
}
