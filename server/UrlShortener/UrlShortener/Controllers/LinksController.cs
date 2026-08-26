using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers;

public class LinksController : Controller
{
    [HttpPost]
    public IActionResult Post(string url)
    {
        return Ok("post");
    }
    
    [HttpGet]
    [Route("/links")]
    public IActionResult GetAllLinks(string url)
    {
        return Ok("GetAllLinks");
    }
    
    [HttpPatch]
    public IActionResult Patch(string url)
    {
        return NoContent();
    }
    
    [HttpDelete]
    public IActionResult Delete(string url)
    {
        return NoContent();
    }
}