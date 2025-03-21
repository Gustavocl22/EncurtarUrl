using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Carregar variáveis de ambiente
builder.Configuration.AddEnvironmentVariables();

var connectionString = builder.Configuration["DefaultConnection"];

// Configuração do banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173" ,"https://encurtarurl.onrender.com")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddScoped<IUrlShortenerRepository, UrlShortenerRepository>();

var app = builder.Build();

app.UseCors("AllowAnyOrigin");

app.MapControllers();

app.UseAuthorization();

app.Run();
