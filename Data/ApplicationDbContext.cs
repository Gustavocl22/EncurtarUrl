
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<UrlShortener> UrlShorteners { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrlShortener>().HasIndex(u => u.ShortenedUrl).IsUnique();
        modelBuilder.Entity<UrlShortener>().HasIndex(u => u.OriginalUrl).IsUnique();
    }
}