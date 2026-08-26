using UrlShortener.Services;

namespace UrlShortener.Models.Http;

public class CreateShortlinkRequestModel
{
    public string Url { get; set; }
    public string? CustomAlias { get; set; }

    
    
}