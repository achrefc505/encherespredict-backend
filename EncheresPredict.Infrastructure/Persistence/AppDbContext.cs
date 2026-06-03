using EncheresPredict.Application.Common.Interfaces;
using EncheresPredict.Domain.Entities;
using EncheresPredict.Infrastructure.Identity;
using EncheresPredict.Infrastructure.Persistence.ReadModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EncheresPredict.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
    public DbSet<Auction> Auctions => Set<Auction>();
    public DbSet<AiAnalysis> AiAnalyses => Set<AiAnalysis>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<Alert> Alerts => Set<Alert>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<DocumentSummary> DocumentSummaries => Set<DocumentSummary>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.Entity<ApplicationUser>(u =>
        {
            u.Property(x => x.FirstName).HasMaxLength(100);
            u.Property(x => x.LastName).HasMaxLength(100);
        });
    }
}
