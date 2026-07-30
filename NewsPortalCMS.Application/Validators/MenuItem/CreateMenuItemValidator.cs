using FluentValidation;
using NewsPortalCMS.Application.DTOs.MenuItem;

namespace NewsPortalCMS.Application.Validators;

public class CreateMenuItemValidator : AbstractValidator<CreateMenuItemDto>
{
    public CreateMenuItemValidator()
    {
        RuleFor(x => x.MenuId)
            .GreaterThan(0);

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Url)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Target)
            .Must(x => x == "_self" || x == "_blank")
            .WithMessage("Target must be '_self' or '_blank'.");
    }
}