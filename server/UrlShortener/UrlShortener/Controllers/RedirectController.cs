using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Interfaces;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Databases;

public class RedirectController(ILinkService linkService,IConfiguration config) : Controller
{
    [HttpGet("{urlCode}")]
    public IActionResult Get(string urlCode)
    {
        try
        {
            LinkModel? linkModel = linkService.Find(urlCode);
            if (linkModel == null)
            {
                return BadRequest();
            }

            if (!UrlService.IsValidUrl(linkModel.LongUrl,config["baseUrl"]))
            {
                return BadRequest();
            }

            linkService.AddCount(linkModel);
            string userAgent = Request.Headers.UserAgent.ToString().ToLower();
            string targetUrl = linkModel.LongUrl;

            if (userAgent.Contains("iphone") || userAgent.Contains("ipad") || userAgent.Contains("ipod"))
            {
                targetUrl = string.IsNullOrWhiteSpace(linkModel.LongAplUrl) ? linkModel.LongUrl : linkModel.LongAplUrl;
            }
            else if (userAgent.Contains("android"))
            {
                targetUrl = string.IsNullOrWhiteSpace(linkModel.LongAndUrl) ? linkModel.LongUrl : linkModel.LongAndUrl;
            }
            return Redirect(targetUrl);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}