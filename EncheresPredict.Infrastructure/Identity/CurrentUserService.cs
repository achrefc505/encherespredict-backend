using System.Security.Claims;
using EncheresPredict.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EncheresPredict.Infrastructure.Identity;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor)
    : ICurrentUserService
{
    public string? UserId =>
        httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
}
