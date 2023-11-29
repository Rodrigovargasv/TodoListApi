using Hangfire;
using TodoList.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();


// Adiconando o serviço de injeção de depêndencia.
builder.Services.AddServices(builder.Configuration);


// Adicionar serviço de política de acesso a Api.
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder
         .AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader();



    });
});


// Altera o formato de data e hora do banco de dados PostgreSql
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


var app = builder.Build();

// Define configuração para inicialização do hangFire.
app.UseHangfireDashboard();
app.UseHttpsRedirection();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuração de middleware: Cors, Routing, Authentication, Authorization, Controllers.
app.UseCors("EnableCORS");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
