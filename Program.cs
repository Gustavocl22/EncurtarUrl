using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Carregar variáveis de ambiente
builder.Configuration.AddEnvironmentVariables();

var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
                       ?? builder.Configuration["DefaultConnection"];

Console.WriteLine($"ConnectionString: {connectionString}");

// Configuração do banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddScoped<IUrlShortenerRepository, UrlShortenerRepository>();

var app = builder.Build();

app.UseCors("AllowAll");

app.MapControllers();

app.UseAuthorization();

app.Run();
