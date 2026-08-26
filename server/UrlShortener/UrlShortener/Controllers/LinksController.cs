using Microsoft.AspNetCore.Mvc;
using UrlShortener.Databases;
using UrlShortener.Interfaces;
using UrlShortener.Models.Http;

namespace UrlShortener.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LinksController(ILinkService linkService) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateShortlinkModel model)
    {
        //TODO return multiple links with stats visited, created and last accessed.
        await linkService.CreateShortUrlAsync(model.Url);
        return Ok("post");
    }
    
    [HttpGet]
    public IActionResult GetAllLinks()
    {
        //TODO return multiple links with stats visited, created and last accessed.
        
        return Ok($"");
    }
    
    [HttpGet("{url}")]
    public IActionResult GetLink(string url)
    {
        //TODO return link stats with visited, created and last accessed.
        return Ok("GetAllLinks");
    }
    
    [HttpPatch("{url}")]
    public IActionResult Patch(string url)
    {
        //TODO Enable link then return nocontent
        return NoContent();
    }
    
    [HttpDelete("{url}")]
    public IActionResult Delete(string url)
    {
        //TODO disable link then return nocontent
        return NoContent();
    }
}