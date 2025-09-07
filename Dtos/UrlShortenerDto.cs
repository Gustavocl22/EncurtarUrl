public class UrlShortenerDto
{
    public required string OriginalUrl { get; set; }
    public string? ShortenedUrl { get; set; } // Permite editar o link encurtado
}
