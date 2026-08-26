using UrlShortener.Models;
using UrlShortener.Models.Http;

namespace UrlShortener.Interfaces;

public interface ILinkService
{
    Task<string> CreateShortUrlAsync(CreateShortlinkRequestModel defaultUrl);
    LinkModel? Find(string urlCode);
    Task AddCount(LinkModel urlCode);
}