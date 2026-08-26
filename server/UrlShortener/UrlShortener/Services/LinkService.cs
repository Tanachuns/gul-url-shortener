using UrlShortener.Databases;
using UrlShortener.Interfaces;
using UrlShortener.Models;
using UrlShortener.Models.Http;

namespace UrlShortener.Services;

public class LinkService:ILinkService
{
    private readonly IUrlGenerateService _urlGenerateService = new UrlGenerateService();
    private readonly LinkContext _context;
    public LinkService(LinkContext context)
    {
        _context = context;
    }
    public async Task<string> CreateShortUrlAsync(CreateShortlinkRequestModel request)
    {
        var link = new LinkModel()
        {
            LongUrl = request.Url,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        _context.Links.Add(link);
        await _context.SaveChangesAsync();
        string shortCode = _urlGenerateService.Generate(link.Id.ToString());
        link.Code = request.CustomAlias??shortCode;
        await _context.SaveChangesAsync();
       
        return link.Code;
    }

    public LinkModel? Find(string urlCode)
    {
        return  _context.Links.FirstOrDefault(l => l.IsActive && l.Code == urlCode);
    }

    public async Task AddCount(LinkModel link)
    {
        LinkModel? _link = await _context.Links.FindAsync(link.Id);
        if (_link == null)
        {
            throw  new Exception("Link not found");
        }
        _link.Visited += 1;
        await _context.SaveChangesAsync();
    }
}