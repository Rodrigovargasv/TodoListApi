using Hangfire;
using System.Text.Json.Serialization;
using TodoList.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddControllers();

builder.Services.AddControllers()
    // Desabilidade mode state
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

// Adicona os serviços de injeção de depêndencia.
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

//Configuração de middleware: hangFire.
app.UseHangfireDashboard();
app.UseHttpsRedirection();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuração de middleware: Cors, Routing, Authentication, Authorization, Controllers, Execptions.
app.UseInfrastructure(builder.Configuration);
app.UseCors("EnableCORS");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Configura uso do Hangfire Dashboard
app.UseHangfireDashboard("/hangfire", new DashboardOptions());

app.MapControllers();

app.Run();
