using System.Text.Json;
using EncheresPredictApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EncheresPredictApi.Features.Auctions;

[ApiController]
[Route("api/auctions/{auctionId}/summary")]
[Authorize]
public class SummaryController : ControllerBase
{
    private readonly AppDbContext _db;

    public SummaryController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetSummary(string auctionId)
    {
        var record = await _db.DocumentSummaries
            .Where(s => s.AuctionId == auctionId)
            .OrderByDescending(s => s.GeneratedAt)
            .FirstOrDefaultAsync();

        if (record is null)
            return NotFound(new { status = "unavailable", message = "Aucun résumé CCV disponible pour ce bien." });

        try
        {
            var parsed = JsonSerializer.Deserialize<JsonElement>(record.SummaryJson);
            return Ok(new
            {
                auctionId = record.AuctionId,
                generatedAt = record.GeneratedAt,
                modelVersion = record.ModelVersion ?? "claude-sonnet-4-6",
                summary = parsed
            });
        }
        catch
        {
            return StatusCode(500, new { message = "Résumé corrompu en base." });
        }
    }
}
