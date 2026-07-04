using EncheresPredict.Application.Common.Interfaces;
using EncheresPredict.Application.Credits.Commands.GrantCredits;
using EncheresPredict.Domain.Repositories;
using MediatR;

namespace EncheresPredict.Application.Credits.Commands.GrantCredits;

public sealed class GrantCreditsCommandHandler(
    ICreditAccountRepository repo,
    IApplicationDbContext context)
    : IRequestHandler<GrantCreditsCommand>
{
    public async Task Handle(
        GrantCreditsCommand command,
        CancellationToken ct)
    {
        var account = await repo.GetByUserIdAsync(
            command.UserId,
            ct);

        if (account is null)
        {
            throw new KeyNotFoundException(
                $"Credit account not found for user '{command.UserId}'.");
        }

        account.Grant(
            command.Amount,
            command.Reason);

        await repo.UpdateAsync(account, ct);
        await context.SaveChangesAsync(ct);
    }
}
