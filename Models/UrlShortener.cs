public class UrlShortener
{
    public int Id { get; set; }
    public required  string OriginalUrl { get; set; }
    public required string ShortenedUrl { get; set; }
    public int Clicks { get; set; }
}
