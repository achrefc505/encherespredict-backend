using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EncheresPredict.Infrastructure.Identity;

public static class IdentitySeedData
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var logger = services.GetRequiredService<ILogger<ApplicationUser>>();
        var configuration = services.GetRequiredService<IConfiguration>();

        foreach (var role in new[] { "Admin", "BetaUser" })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                logger.LogInformation("Role '{Role}' created.", role);
            }
        }

        const string adminEmail = "admin@encherespredict.fr";

        var adminPassword =
            configuration["Admin:Password"]
            ?? throw new InvalidOperationException(
                "Admin:Password is missing.");

        if (await userManager.FindByEmailAsync(adminEmail) is not null)
        {
            return;
        }

        var admin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "Admin",
            LastName = "EP",
            EmailConfirmed = true,
            IsAdmin = true,
            BetaExpiresAt = DateTime.UtcNow.AddYears(10)
        };

        var result = await userManager.CreateAsync(admin, adminPassword);

        if (!result.Succeeded)
        {
            logger.LogError(
                "Admin account creation failed: {Errors}",
                string.Join(", ", result.Errors.Select(e => e.Description)));

            return;
        }

        await userManager.AddToRoleAsync(admin, "Admin");

        logger.LogInformation(
            "Admin account created: {Email}",
            adminEmail);
    }
}