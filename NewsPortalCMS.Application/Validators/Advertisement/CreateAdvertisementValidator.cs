using FluentValidation;
using NewsPortalCMS.Application.DTOs.Advertisement;

namespace NewsPortalCMS.Application.Validators.Advertisement
{
    public class CreateAdvertisementValidator
        : AbstractValidator<CreateAdvertisementDto>
    {
        public CreateAdvertisementValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Advertisement title is required.")
                .MaximumLength(200)
                .WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.BannerFile)
                .NotNull()
                .WithMessage("Advertisement banner is required.");

            RuleFor(x => x.BannerFile)
                .Must(file =>
                    file == null ||
                    file.Length <= 5 * 1024 * 1024)
                .WithMessage("Banner size cannot exceed 5 MB.");

            RuleFor(x => x.BannerFile)
                .Must(file =>
                    file == null ||
                    file.ContentType == "image/jpeg" ||
                    file.ContentType == "image/png" ||
                    file.ContentType == "image/webp" ||
                    file.ContentType == "image/jpg")
                .WithMessage("Only JPG, JPEG, PNG and WEBP images are allowed.");

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required.");

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("End date is required.");

            RuleFor(x => x)
                .Must(x => x.EndDate >= x.StartDate)
                .WithMessage(
                    "End date must be greater than or equal to start date."
                );

            RuleFor(x => x.RedirectUrl)
                .MaximumLength(500)
                .When(x => !string.IsNullOrEmpty(x.RedirectUrl))
                .WithMessage(
                    "Redirect URL cannot exceed 500 characters."
                );
        }
    }
}