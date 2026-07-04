using EncheresPredict.Application.Common.Interfaces;
using EncheresPredict.Domain.Repositories;
using MediatR;

namespace EncheresPredict.Application.Credits.Commands.ConsumeCredit;

public sealed class ConsumeCreditCommandHandler(
    ICreditAccountRepository repo,
    ICurrentUserService currentUser,
    IApplicationDbContext context)
    : IRequestHandler<ConsumeCreditCommand>
{
    public async Task Handle(
        ConsumeCreditCommand command,
        CancellationToken ct)
    {
        var userId = currentUser.UserId
            ?? throw new UnauthorizedAccessException(
                "User is not authenticated.");

        var account = await repo.GetByUserIdAsync(
            userId,
            ct);

        if (account is null)
        {
            throw new KeyNotFoundException(
                $"Credit account not found for user '{userId}'.");
        }

        account.Consume(command.Reason);

        await repo.UpdateAsync(account, ct);

        await context.SaveChangesAsync(ct);
    }
}