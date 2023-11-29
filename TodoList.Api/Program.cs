using Hangfire;
using TodoList.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();


// Adiconando o servi�o de inje��o de dep�ndencia.
builder.Services.AddServices(builder.Configuration);


// Adicionar servi�o de pol�tica de acesso a Api.
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

// Define configura��o para inicializa��o do hangFire.
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
