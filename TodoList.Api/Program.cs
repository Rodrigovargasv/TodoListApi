using Hangfire;
using TodoList.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiconando o servi�o de inje��o de dep�ndencia.
builder.Services.AddServices(builder.Configuration);

builder.Services.AddControllers();

// Adicionar servi�o de pol�tica de acesso a Api.
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.WithOrigins("https://todo-list-web-nine.vercel.app")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();


app.Run();
