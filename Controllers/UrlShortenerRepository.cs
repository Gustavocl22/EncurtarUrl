using Microsoft.EntityFrameworkCore;

public class UrlShortenerRepository : IUrlShortenerRepository
{
    private readonly ApplicationDbContext _context;

    public UrlShortenerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UrlShortener> GetUrlByIdAsync(int id)
    {
        return await _context.UrlShorteners.FindAsync(id);
    }

    public async Task<List<UrlShortener>> GetAllUrlsAsync()
    {
        return await _context.UrlShorteners.ToListAsync();
    }

    public async Task<UrlShortener> GetUrlByOriginalUrlAsync(string originalUrl)
    {
        return await _context.UrlShorteners.FirstOrDefaultAsync(u => u.OriginalUrl == originalUrl);
    }

    public async Task<UrlShortener> GetUrlByShortenedUrlAsync(string shortenedUrl)
    {
        return await _context.UrlShorteners.FirstOrDefaultAsync(u => u.ShortenedUrl == shortenedUrl);
    }

    public async Task AddUrlShortenerAsync(UrlShortener urlShortener)
    {
        await _context.UrlShorteners.AddAsync(urlShortener);
    }

    public async Task DeleteUrlShortenerAsync(UrlShortener urlShortener)
    {
        _context.UrlShorteners.Remove(urlShortener);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
