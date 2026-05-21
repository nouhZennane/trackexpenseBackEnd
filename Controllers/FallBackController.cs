using Azure;
using Microsoft.AspNetCore.Mvc;
using TrackExences.Services;

namespace TrackExences.Controllers;


[ApiController]
[Route("api/v1/{**path}")]
public class FallBackController(IWebHostEnvironment env): ControllerBase
{
    [HttpGet, HttpPost, HttpPatch, HttpPut, HttpDelete, HttpHead, HttpOptions]
    public IActionResult Handle(string path)
    {
        var response = ResponseService.Handle(HttpContext.Request, env.IsDevelopment());
        return NotFound(response);
    }
}