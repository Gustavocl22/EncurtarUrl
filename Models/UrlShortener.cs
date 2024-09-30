public class UrlShortener : IUrlShortener
{
    public int Id { get; set; }
    public string OriginalUrl { get; set; }
    public string ShortenedUrl { get; set; }
    public int Clicks { get; set; }

    public string GenerateShortenedUrl()
    {
        return Guid.NewGuid().ToString().Substring(0, 8); 
    }

    public void IncrementClicks()
    {
        Clicks++;
    }
}