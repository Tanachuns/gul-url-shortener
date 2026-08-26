using UrlShortener.Models;

namespace UrlShortener.Interfaces;

public interface ILinkService
{
    Task<string> CreateShortUrlAsync(string defaultUrl);
    LinkModel? Find(string urlCode);
    
}