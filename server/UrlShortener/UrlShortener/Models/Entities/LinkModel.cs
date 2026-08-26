namespace UrlShortener.Models;

public class LinkModel
{
    public int Id { get; set; }
    public string? LongUrl { get; set; }
    public string? LongAplUrl { get; set; }
    public string? LongAndUrl { get; set; }
    public string? Code { get; set; }
    public int Visited { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastAccessed { get; set; } 
    public DateTime UpdatedAt { get; set; } 
    public bool IsActive { get; set; }
}