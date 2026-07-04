using FluentValidation;

namespace EncheresPredict.Application.Credits.Commands.GrantCredits;

public sealed class GrantCreditsCommandValidator
    : AbstractValidator<GrantCreditsCommand>
{
    public GrantCreditsCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Amount)
            .GreaterThan(0);

        RuleFor(x => x.Reason)
            .NotEmpty()
            .MaximumLength(200);
    }
}