using EncheresPredict.Application.Common.Interfaces;
using EncheresPredict.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EncheresPredict.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Auction> Auctions => Set<Auction>();
    public DbSet<AiAnalysis> AiAnalyses => Set<AiAnalysis>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<Alert> Alerts => Set<Alert>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}
