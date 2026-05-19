using FluentValidation;
using Photo.Application.DTOs;

namespace Photo.Application.Validators;

public sealed class CreatePhotoRequestValidator
    : AbstractValidator<CreatePhotoRequest>
{
    public CreatePhotoRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Url)
            .NotEmpty()
            .Must(BeValidUrl)
            .WithMessage("Invalid URL");
    }

    private static bool BeValidUrl(string url)
    {
        return Uri.TryCreate(
            url,
            UriKind.Absolute,
            out _);
    }
}