namespace UrlShortener.Models.Http;

public class CreateShortlinkRequestModel
{
    public string Url { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(Url)&&!string.IsNullOrWhiteSpace(Url);
    }
    
}