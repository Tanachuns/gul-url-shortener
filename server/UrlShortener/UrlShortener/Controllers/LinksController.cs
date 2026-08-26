using Microsoft.AspNetCore.Http.HttpResults;
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
    public async Task<IActionResult> Post([FromBody] CreateShortlinkRequestModel requestModel)
    {
         CreateShortlinkResponseModel responseModel = new CreateShortlinkResponseModel();
        
        //TODO create short url and return a result
        try
        {
            if (!requestModel.IsValid())
            {
                return BadRequest("Invalid Url Format.");
            }
            string shortCode = await linkService.CreateShortUrlAsync(requestModel.Url);
            string beseUrl = $"https://{Request.Host}";//get baseurl form appsettings
            responseModel.Shortlink = $"{beseUrl}/{shortCode}"; 
            responseModel.Success = true;
            return Ok(responseModel);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
   
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