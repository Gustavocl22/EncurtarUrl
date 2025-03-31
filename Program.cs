using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Carrega .env em desenvolvimento
if (builder.Environment.IsDevelopment())
{
    DotNetEnv.Env.Load();
}

var connectionString = builder.Configuration.GetConnectionString("PostgreSQL")
    .Replace("${DB_HOST}", Environment.GetEnvironmentVariable("DB_HOST"))
    .Replace("${DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME"))
    .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER"))
    .Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD"));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString)
);

// Restante da configuração...
builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowAnyOrigin",
        policy => policy.WithOrigins("http://localhost:5173")
                       .AllowAnyHeader()
                       .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddScoped<IUrlShortenerRepository, UrlShortenerRepository>();

var app = builder.Build();

// Aplicar migrações automaticamente
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Database connection string is not configured");
}

app.UseCors("AllowAnyOrigin");
app.MapControllers();
app.UseAuthorization();
app.Run();