using EncheresPredictApi.Models;
using Microsoft.AspNetCore.Identity;

namespace EncheresPredictApi.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var logger = services.GetRequiredService<ILogger<AppDbContext>>();

        // Roles
        foreach (var role in new[] { "Admin", "BetaUser" })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                logger.LogInformation("Rôle '{Role}' créé.", role);
            }
        }

        // Admin
        const string adminEmail = "admin@encherespredict.fr";
        const string adminPassword = "Admin@EP2026!";

        if (await userManager.FindByEmailAsync(adminEmail) is null)
        {
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
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
                logger.LogInformation("Compte admin créé : {Email}", adminEmail);
            }
            else
            {
                logger.LogError("Échec création admin : {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}
