using FluentValidation;

namespace EncheresPredict.Application.Auctions.Commands.UploadCcv;

public sealed class UploadCcvCommandValidator
    : AbstractValidator<UploadCcvCommand>
{
    public UploadCcvCommandValidator()
    {
        RuleFor(x => x.AuctionId)
            .NotEmpty();

        RuleFor(x => x.PdfContent)
            .NotNull();

        RuleFor(x => x.PdfContent.Length)
            .GreaterThan(0)
            .LessThanOrEqualTo(20 * 1024 * 1024)
            .WithMessage("Le fichier ne doit pas dépasser 20 Mo.");

        RuleFor(x => x.FileName)
            .NotEmpty()
            .Must(fileName =>
                Path.GetExtension(fileName)
                    .Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Le fichier doit être un PDF.");
    }
}