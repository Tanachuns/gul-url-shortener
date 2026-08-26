using UrlShortener.Databases;
using UrlShortener.Interfaces;
using UrlShortener.Models;

namespace UrlShortener.Services;

public class LinkService:ILinkService
{
    private readonly LinkContext _context;
    public LinkService(LinkContext context)
    {
        _context = context;
    }
    public async Task<string> CreateShortUrlAsync(string defaultUrl)
    {
        var link = new LinkModel()
        {
            LongUrl = defaultUrl,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        _context.Links.Add(link);
        await _context.SaveChangesAsync();
        Base62.Base62Converter base62Converter = new Base62.Base62Converter();
        string shortCode = base62Converter.Encode(link.Id.ToString());
        link.Code = shortCode;
        await _context.SaveChangesAsync();
       
        return shortCode;
    }
}