namespace UrlShortener.Interfaces;

public interface ILinkService
{
    Task<string> CreateShortUrlAsync(string defaultUrl);
}