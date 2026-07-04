using EncheresPredict.Application.Common.Interfaces;
using EncheresPredict.Domain.Entities;
using EncheresPredict.Domain.Repositories;
using MediatR;

namespace EncheresPredict.Application.Credits.Commands.InitializeCreditAccount;

public sealed class InitializeCreditAccountCommandHandler(
    ICreditAccountRepository repo,
    IApplicationDbContext context)
    : IRequestHandler<InitializeCreditAccountCommand>
{
    public async Task Handle(
        InitializeCreditAccountCommand command,
        CancellationToken ct)
    {
        var existing = await repo.GetByUserIdAsync(
            command.UserId,
            ct);

        if (existing is not null)
        {
            return;
        }

        var account = CreditAccount.Create(command.UserId);

        await repo.AddAsync(account, ct);

        await context.SaveChangesAsync(ct);
    }
}