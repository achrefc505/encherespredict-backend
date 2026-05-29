using System.ComponentModel.DataAnnotations;

namespace EncheresPredictApi.Features.Auth;

public record RegisterRequest(
    [Required, EmailAddress] string Email,
    [Required, MinLength(8)] string Password,
    [Required, MaxLength(100)] string FirstName,
    [MaxLength(100)] string? LastName
);

public record LoginRequest(
    [Required, EmailAddress] string Email,
    [Required] string Password
);
