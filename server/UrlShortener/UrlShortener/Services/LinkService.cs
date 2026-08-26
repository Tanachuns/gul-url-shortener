using UrlShortener.Databases;
using UrlShortener.Interfaces;
using UrlShortener.Models;
using UrlShortener.Models.Http;

namespace UrlShortener.Services;

public class LinkService(LinkContext context) : ILinkService
{
    private readonly IUrlGenerateService _urlGenerateService = new UrlGenerateService();

    public async Task<string> CreateShortUrlAsync(CreateShortlinkRequestModel request)
    {
        var link = new LinkModel()
        {
            LongUrl = request.Url,
            LongAplUrl =  request.iosUrl,
            LongAndUrl =  request.androidUrl,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        context.Links.Add(link);
        await context.SaveChangesAsync();
        string shortCode = _urlGenerateService.Generate(link.Id.ToString());
        link.Code = request.CustomAlias??shortCode;
        await context.SaveChangesAsync();
        return link.Code;


    }

    public LinkModel? Find(string urlCode,bool? isActive)
    {
        if (isActive == null)
        {
            return  context.Links.FirstOrDefault(l => l.Code == urlCode);
        }
        return  context.Links.FirstOrDefault(l => l.IsActive==isActive && l.Code == urlCode);
    }
    public List<LinkModel> FindAll()
    {
        return context.Links.ToList();
    }

    public async Task AddCount(LinkModel link)
    {
        LinkModel? _link = await context.Links.FindAsync(link.Id);
        if (_link == null)
        {
            throw  new Exception("Link not found");
        }
        _link.Visited += 1;
        _link.LastAccessed = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }

    public async Task SetActive(LinkModel link ,bool isActive)
    {
        LinkModel? _link = context.Links.FirstOrDefault(l=>l.Id==link.Id && l.IsActive!= isActive);
        if (_link == null)
        {
            throw  new Exception("Link not found");
        }
        _link.IsActive = isActive;
        await context.SaveChangesAsync();
    }
}