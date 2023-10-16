using Hangfire;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using TodoList.Domain.Entities;
using TodoList.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiconando o serviço de injeção de depêndencia.
builder.Services.AddServices(builder.Configuration);

builder.Services.AddControllers();

// Adicionar serviço de política de acesso a Api.
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.WithOrigins("http://localhost:3000", "https://todo-list-web-nine.vercel.app")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


// Adiciona configuração do kestrel, onde a api escutará nas portas 5010 para http e 5011 para https.
builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(5010);
    options.ListenAnyIP(5011, listenOptions => listenOptions.UseHttps());
});


var app = builder.Build();



AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();
app.UseHttpsRedirection();

app.MapControllers();
app.UseCors("EnableCORS");


app.Run();
