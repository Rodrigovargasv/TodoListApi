using Hangfire;
using Microsoft.OpenApi.Models;
using TodoList.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// Adicionado o servi�o do Swagger para que ele aceite autentica��o com JWT bearer.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoListApi", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Header de autoriza��o JWT usando o esquema Bearer. \r\n\r\nInforme 'Bearer' e o seu token. \r\n\r\nExamplo: \'Bearer 1234abcdef\'"

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


// Altera o formato de data e hora do banco de dados PostgreSql
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Define configura��o para inicializa��o do hangFire.
app.UseHangfireDashboard();
app.UseHttpsRedirection();

app.MapControllers();

// Define a configura��o de pol�ticas de acesso a api.
app.UseCors("EnableCORS");

// Define a confgiura��o para autentica��o e autoriza��o da Api.
app.UseAuthentication();
app.UseAuthorization();


app.Run();
