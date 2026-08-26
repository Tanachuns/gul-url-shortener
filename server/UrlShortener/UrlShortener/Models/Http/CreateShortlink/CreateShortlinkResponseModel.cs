namespace UrlShortener.Models.Http;

public class CreateShortlinkResponseModel:BaseResponseModel
{
    public string? Shortlink { get; set; }
}