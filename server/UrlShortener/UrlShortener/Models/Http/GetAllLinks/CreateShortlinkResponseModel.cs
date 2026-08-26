namespace UrlShortener.Models.Http;

public class GetAllLinksResponseModel:BaseResponseModel
{
    public List<LinkModel> Response { get; set; }
}