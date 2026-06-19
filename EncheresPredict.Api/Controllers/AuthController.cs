using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EncheresPredict.Api.Contracts.Auth;
using EncheresPredict.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EncheresPredict.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(UserManager<ApplicationUser> userManager, IConfiguration config) : ControllerBase

{
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            BetaExpiresAt = DateTime.UtcNow.AddDays(30)
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
        }

        await userManager.AddToRoleAsync(user, "BetaUser");
        return Ok(new { message = "Compte cree. Bienvenue dans la beta !" });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            return Unauthorized(new { message = "Email ou mot de passe incorrect." });
        }

        if (!user.IsAdmin && user.BetaExpiresAt < DateTime.UtcNow)
        {
            return StatusCode(403, new { message = "Votre acces beta de 30 jours a expire." });
        }

        return Ok(new
        {
            token = GenerateJwt(user),
            expiresAt = DateTime.UtcNow.AddDays(7),
            user = new { user.Email, user.FirstName, user.LastName, user.IsAdmin }
        });
    }

    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Me()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await userManager.FindByIdAsync(userId!);
        if (user is null)
        {
            return NotFound();
        }

        if (!user.IsAdmin && user.BetaExpiresAt < DateTime.UtcNow)
        {
            return StatusCode(403, new { message = "Acces beta expire." });
        }

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
        var jwtKey = config["Jwt:Key"]
            ?? throw new InvalidOperationException("Jwt:Key is missing.");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim("firstName", user.FirstName ?? string.Empty),
            new Claim("isAdmin", user.IsAdmin.ToString().ToLowerInvariant()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
