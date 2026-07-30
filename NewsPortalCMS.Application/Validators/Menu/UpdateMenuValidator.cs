using FluentValidation;
using NewsPortalCMS.Application.DTOs.Menu;

namespace NewsPortalCMS.Application.Validators;

public class UpdateMenuValidator : AbstractValidator<UpdateMenuDto>
{
    public UpdateMenuValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Location)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .MaximumLength(250)
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}