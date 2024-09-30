public interface IUrlShortener
{
    int Id { get; set; }
    string OriginalUrl { get; set; }
    string ShortenedUrl { get; set; }
    int Clicks { get; set; }

    string GenerateShortenedUrl(); 
    void IncrementClicks();
}