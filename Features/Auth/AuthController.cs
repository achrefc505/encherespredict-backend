using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EncheresPredictApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EncheresPredictApi.Features.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _config;

    public AuthController(UserManager<ApplicationUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req)
    {
        var user = new ApplicationUser
        {
            UserName = req.Email,
            Email = req.Email,
            FirstName = req.FirstName,
            LastName = req.LastName,
            BetaExpiresAt = DateTime.UtcNow.AddDays(30)
        };

        var result = await _userManager.CreateAsync(user, req.Password);
        if (!result.Succeeded)
            return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

        await _userManager.AddToRoleAsync(user, "BetaUser");
        return Ok(new { message = "Compte créé. Bienvenue dans la beta !" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var user = await _userManager.FindByEmailAsync(req.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, req.Password))
            return Unauthorized(new { message = "Email ou mot de passe incorrect." });

        if (!user.IsAdmin && user.BetaExpiresAt < DateTime.UtcNow)
            return StatusCode(403, new { message = "Votre accès beta de 30 jours a expiré." });

        var token = GenerateJwt(user);
        return Ok(new
        {
            token,
            expiresAt = DateTime.UtcNow.AddDays(7),
            user = new { user.Email, user.FirstName, user.LastName, user.IsAdmin }
        });
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId!);
        if (user is null) return NotFound();

        if (!user.IsAdmin && user.BetaExpiresAt < DateTime.UtcNow)
            return StatusCode(403, new { message = "Accès beta expiré." });

        return Ok(new
        {
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName,
            user.BetaExpiresAt,
            user.IsAdmin,
            betaDaysLeft = Math.Max(0, (int)(user.BetaExpiresAt - DateTime.UtcNow).TotalDays)
        });
    }

    private string GenerateJwt(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim("firstName", user.FirstName ?? ""),
            new Claim("isAdmin", user.IsAdmin.ToString().ToLowerInvariant()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
