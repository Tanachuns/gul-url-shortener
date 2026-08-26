namespace UrlShortener.Models.Http;

public class GetlinkResponseModel:BaseResponseModel
{
    public LinkModel? Response { get; set; }
}