namespace UrlShortener.Interfaces;

public partial interface IUrlGenerateService
{
    public string Generate(string id);
}