using UrlShortener.Services;

namespace UrlShortener.Models.Http;

public class CreateShortlinkRequestModel
{
    public string Url { get; set; }
    public string? iosUrl { get; set; }
    public string? androidUrl { get; set; }
    public string? CustomAlias { get; set; }

    
    
}