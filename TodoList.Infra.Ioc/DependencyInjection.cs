
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
using TodoList.Application.Mappings;
using TodoList.Domain.Validation;
using Microsoft.OpenApi.Models;

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

            #region habilita sistema agenda envios dos emails
            services.AddHangfire(x => x.UseMemoryStorage());
            services.AddHangfireServer();
            #endregion


            services.AddAutoMapper(typeof(MappingsEntityDTOs));

            #region serviço
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISendEmail, SendEmailService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRecoveryPasswordUserRepository, RecoveryPasswordRepository>();
            services.AddScoped<IRecoveryPasswordUserService, RecoveryPassawordUserService>();

            services.AddScoped<GenerationCodeRecoveryService>();
            services.AddScoped<JobValidation>();
            services.AddTransient<TokenService>();
            #endregion


            // faz o armazenamento de dados em cache.
            services.AddMemoryCache();


            #region Configurações do Swagger

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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                        //ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("admin", p => p.RequireRole("admin"));
                option.AddPolicy("commonUser", p => p.RequireRole("commonUser"));

            });
           
            #endregion


            return services;
        }
    }
}
