using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UrlShortenerController : ControllerBase
{
    private readonly IUrlShortenerRepository _repository;

    public UrlShortenerController(IUrlShortenerRepository repository)
    {
        _repository = repository;
    }

    // GET: api/urlshortener
    [HttpGet]
    public async Task<IActionResult> GetAllUrls()
    {
        var urls = await _repository.GetAllUrlsAsync();
        return Ok(urls);
    }
    // GET: api/urlshortener/{shortenedUrl}
    [HttpGet("{shortenedUrl}")]
    public async Task<IActionResult> RedirectToOriginalUrl(string shortenedUrl)
    {
        var urlShortener = await _repository.GetUrlByShortenedUrlAsync(shortenedUrl);
        if (urlShortener == null)
        {
            return NotFound();
        }

        
        urlShortener.Clicks++;
        await _repository.SaveChangesAsync();

        
        return Redirect(urlShortener.OriginalUrl);
    }

    // POST: api/urlshortener
    [HttpPost]
    public async Task<IActionResult> ShortenUrl([FromBody] UrlShortenerDto urlShortenerDto)
    {
        if (!Uri.IsWellFormedUriString(urlShortenerDto.OriginalUrl, UriKind.Absolute))
        {
            return BadRequest("Invalid URL format.");
        }

        var existingUrl = await _repository.GetUrlByOriginalUrlAsync(urlShortenerDto.OriginalUrl);

        if (existingUrl != null)
        {
            return Ok(new { id = existingUrl.Id, shortenedUrl = existingUrl.ShortenedUrl });
        }

        var urlShortener = new UrlShortener
        {
            OriginalUrl = urlShortenerDto.OriginalUrl,
            ShortenedUrl = GenerateShortenedUrl(),
            Clicks = 0
        };

        await _repository.AddUrlShortenerAsync(urlShortener);
        await _repository.SaveChangesAsync();

        return Ok(new { id = urlShortener.Id, shortenedUrl = urlShortener.ShortenedUrl });
    }

    // DELETE: api/urlshortener/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUrl(int id)
    {
        var urlShortener = await _repository.GetUrlByIdAsync(id);
        if (urlShortener == null)
        {
            return NotFound();
        }

        await _repository.DeleteUrlShortenerAsync(urlShortener);
        await _repository.SaveChangesAsync();

        return NoContent();
    }

    private string GenerateShortenedUrl()
    {
        return Guid.NewGuid().ToString().Substring(0, 8);
    }
}
