using EncheresPredict.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EncheresPredict.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Auction> Auctions { get; }
    DbSet<AiAnalysis> AiAnalyses { get; }
    DbSet<Document> Documents { get; }
    DbSet<Alert> Alerts { get; }
    DbSet<UserProfile> UserProfiles { get; }

    DbSet<DocumentSummary> DocumentSummaries { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}