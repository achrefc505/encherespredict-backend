using EncheresPredictApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EncheresPredictApi.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<DocumentSummary> DocumentSummaries { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(u =>
        {
            u.Property(x => x.FirstName).HasMaxLength(100);
            u.Property(x => x.LastName).HasMaxLength(100);
        });
    }
}
