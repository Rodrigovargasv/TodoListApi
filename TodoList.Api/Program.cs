using Hangfire;
using System.Text.Json.Serialization;
using TodoList.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddControllers();

builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

// Adicona os servi�os de inje��o de dep�ndencia.
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

//Configura��o de middleware: hangFire.
app.UseHangfireDashboard();
app.UseHttpsRedirection();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configura��o de middleware: Cors, Routing, Authentication, Authorization, Controllers.
app.UseCors("EnableCORS");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
