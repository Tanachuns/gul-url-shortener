using UrlShortener.Models;
using UrlShortener.Models.Http;

namespace UrlShortener.Interfaces;

public interface ILinkService
{
    Task<string> CreateShortUrlAsync(CreateShortlinkRequestModel defaultUrl);
    LinkModel? Find(string urlCode,bool isActive=true);
    Task AddCount(LinkModel link);
    List<LinkModel> FindAll();
    Task SetActive(LinkModel link,bool isActive);
}