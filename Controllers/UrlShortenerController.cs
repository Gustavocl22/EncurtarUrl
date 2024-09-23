using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UrlShortenerController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UrlShortenerController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> ShortenUrl([FromBody] UrlShortenerDto urlShortenerDto)
    {
        if (!Uri.IsWellFormedUriString(urlShortenerDto.OriginalUrl, UriKind.Absolute))
        {
            return BadRequest("Invalid URL format.");
        }

        var existingUrl = await _context.UrlShorteners
            .FirstOrDefaultAsync(u => u.OriginalUrl == urlShortenerDto.OriginalUrl);

        if (existingUrl != null)
        {
            return Ok(new { shortenedUrl = existingUrl.ShortenedUrl });
        }

        var urlShortener = new UrlShortener
        {
            OriginalUrl = urlShortenerDto.OriginalUrl,
            ShortenedUrl = GenerateShortenedUrl(),
            Clicks = 0
        };

        try
        {
            await _context.UrlShorteners.AddAsync(urlShortener);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

        return Ok(new { shortenedUrl = urlShortener.ShortenedUrl });
    }

    [HttpGet]
    public async Task<IActionResult> GetUrls()
    {
        var urls = await _context.UrlShorteners.ToListAsync();
        return Ok(urls);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUrl(int id)
    {
        var urlShortener = await _context.UrlShorteners.FindAsync(id);
        if (urlShortener == null)
        {
            return NotFound();
        }

        _context.UrlShorteners.Remove(urlShortener);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{shortenedUrl}")]
    public async Task<IActionResult> RedirectToUrl(string shortenedUrl)
    {
        var urlShortener = await _context.UrlShorteners
            .FirstOrDefaultAsync(u => u.ShortenedUrl == shortenedUrl);

        if (urlShortener == null)
        {
            return NotFound();
        }

        urlShortener.Clicks++; 
        await _context.SaveChangesAsync();

        return Redirect(urlShortener.OriginalUrl); 
    }

    private string GenerateShortenedUrl()
    {
        string shortUrl;
        do
        {
            shortUrl = Guid.NewGuid().ToString().Substring(0, 8);
        }
        while (_context.UrlShorteners.Any(u => u.ShortenedUrl == shortUrl));

        return shortUrl;
    }
}
