using Microsoft.AspNetCore.Mvc;
using UrlShortener.Databases;

namespace UrlShortener.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LinksController : Controller
{
    [HttpPost]
    public IActionResult Post(string url)
    {
        //TODO return multiple links with stats visited, created and last accessed.
        return Ok("post");
    }
    
    [HttpGet]
    public IActionResult GetAllLinks()
    {
        //TODO return multiple links with stats visited, created and last accessed.
        using var db = new LinkContext();
        return Ok($"Database path: {db.DbPath}.");
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