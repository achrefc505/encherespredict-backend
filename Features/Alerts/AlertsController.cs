using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EncheresPredictApi.Features.Alerts;

[ApiController]
[Route("api/alerts")]
[Authorize]
public class AlertsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAlerts() => Ok(Array.Empty<object>());

    [HttpPatch("{id}/read")]
    public IActionResult MarkRead(string id) => NoContent();
}
