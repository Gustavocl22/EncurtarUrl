using System.ComponentModel.DataAnnotations;

public class UrlShortenerDto
{
    [Required]
    public string OriginalUrl { get; set; }
}