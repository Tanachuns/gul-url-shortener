using UrlShortener.Interfaces;

namespace UrlShortener.Services;

public class UrlGenerateService : IUrlGenerateService
{
    public string Generate(string id)
    {
        Base62.Base62Converter base62Converter = new Base62.Base62Converter();
        string shortCode = base62Converter.Encode(id);
        return shortCode;
    }
}