using FluentValidation;
using NewsPortalCMS.Application.DTOs.WebsiteSetting;

namespace NewsPortalCMS.Application.Validators.WebsiteSetting;

public class WebsiteSettingCreateValidator
    : AbstractValidator<WebsiteSettingCreateDto>
{
    public WebsiteSettingCreateValidator()
    {
        RuleFor(x => x.WebsiteName)
            .NotEmpty()
            .WithMessage("Website name is required.")
            .MaximumLength(100)
            .WithMessage("Website name cannot exceed 100 characters.");


        RuleFor(x => x.ContactEmail)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.ContactEmail))
            .WithMessage("Invalid email address.");


        RuleFor(x => x.ContactPhone)
            .MaximumLength(20)
            .When(x => !string.IsNullOrEmpty(x.ContactPhone));


        RuleFor(x => x.MetaTitle)
            .MaximumLength(160)
            .When(x => !string.IsNullOrEmpty(x.MetaTitle));


        RuleFor(x => x.MetaDescription)
            .MaximumLength(300)
            .When(x => !string.IsNullOrEmpty(x.MetaDescription));
    }
}