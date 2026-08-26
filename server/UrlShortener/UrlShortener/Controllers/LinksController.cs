using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Databases;
using UrlShortener.Interfaces;
using UrlShortener.Models;
using UrlShortener.Models.Http;
using UrlShortener.Services;

namespace UrlShortener.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LinksController(ILinkService linkService,IConfiguration config) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateShortlinkRequestModel requestModel)
    {
         CreateShortlinkResponseModel responseModel = new CreateShortlinkResponseModel();
        //TODO Platform-specific destination
        try
        {
            if (!UrlService.IsValidUrl(requestModel.Url, config["baseUrl"]))
            {
                responseModel.Success = false;
                responseModel.Message = "Invalid request";
                return BadRequest(responseModel);
            }

            if (!string.IsNullOrEmpty(requestModel.CustomAlias)&&linkService.Find(requestModel.CustomAlias) != null)
            {
                responseModel.Success = false;
                responseModel.Message = "Invalid Custom alias";
                return BadRequest(responseModel);
            }
            
            string shortCode = await linkService.CreateShortUrlAsync(requestModel);
            if (string.IsNullOrEmpty(config["baseUrl"]))
            {
                throw new Exception("BaseUrl not set");
            }
            string? beseUrl = config["baseUrl"]; // $"https://{Request.Host}";//get baseurl form appsettings
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
        GetAllLinksResponseModel responseModel = new GetAllLinksResponseModel();
        try
        {
            List<LinkModel> linkModels = linkService.FindAll();

            responseModel.Response = linkModels;
            responseModel.Success = true;
            return Ok(responseModel);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
    
    [HttpGet("{url}")]
    public IActionResult GetLink(string url)
    {
        var link = new
        {
            Url = url,
            Visited = 10,
            Created = DateTime.UtcNow.AddDays(-5),
            LastAccessed = DateTime.UtcNow.AddMinutes(-10)
        };

        return Ok(link);
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