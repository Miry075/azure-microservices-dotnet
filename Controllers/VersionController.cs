using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wpm.Managemnt.Api.Dtos;

namespace Wpm.Managemnt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VersionController : ControllerBase
{
    /// <summary>
    /// Get API version information (no authentication required)
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetVersion()
    {
        var versionInfo = new ApiVersionDto
        {
            Version = "1.0.0",
            Name = "WPM Management API",
            BuildDate = new DateTime(2026, 2, 16),
            Status = "Running"
        };

        return Ok(versionInfo);
    }
}
