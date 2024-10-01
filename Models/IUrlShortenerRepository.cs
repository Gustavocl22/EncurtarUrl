public interface IUrlShortenerRepository
{
    Task<UrlShortener> GetUrlByIdAsync(int id);
    Task<List<UrlShortener>> GetAllUrlsAsync();
    Task<UrlShortener> GetUrlByOriginalUrlAsync(string originalUrl);
    Task<UrlShortener> GetUrlByShortenedUrlAsync(string shortenedUrl); 
    Task AddUrlShortenerAsync(UrlShortener urlShortener);
    Task DeleteUrlShortenerAsync(UrlShortener urlShortener);
    Task SaveChangesAsync();
}
