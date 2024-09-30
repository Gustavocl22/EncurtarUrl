using Microsoft.EntityFrameworkCore;
using dotenv.net; 

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


builder.Services.AddControllers();
var app = builder.Build();
app.UseCors("AllowLocalhost");
app.MapControllers();

app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        
        context.Response.StatusCode = 500; 
        await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
    }
});

app.UseAuthorization();
app.Run();
