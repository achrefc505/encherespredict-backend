using FluentValidation;

namespace EncheresPredict.Application.Credits.Commands.ConsumeCredit;

public sealed class ConsumeCreditCommandValidator
    : AbstractValidator<ConsumeCreditCommand>
{
    public ConsumeCreditCommandValidator()
    {
        RuleFor(x => x.Reason)
            .NotEmpty()
            .MaximumLength(200);
    }
}