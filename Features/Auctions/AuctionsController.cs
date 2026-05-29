using EncheresPredictApi.Models.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EncheresPredictApi.Features.Auctions;

// TODO S4 : remplacer le mock par de vraies requêtes sur la DB du scraper
[ApiController]
[Route("api/auctions")]
[Authorize]
public class AuctionsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAuctions([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        => Ok(new AuctionsResult([], 0, page, pageSize));

    [HttpGet("stats")]
    public IActionResult GetStats()
        => Ok(new DashboardStats(0, 0, 0, 0, new BadgeCounts(), []));

    [HttpGet("{id}")]
    public IActionResult GetAuction(string id)
        => NotFound(new { message = "Pipeline data (S4) non encore connecté." });
}
