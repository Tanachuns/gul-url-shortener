using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Interfaces;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Databases;

public class RedirectController(ILinkService linkService) : Controller
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

            if (!UrlService.IsValidUrl(linkModel.LongUrl,"localhost"))
            {
                return BadRequest();
            }

            linkService.AddCount(linkModel);
            
            return Redirect(linkModel.LongUrl);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}