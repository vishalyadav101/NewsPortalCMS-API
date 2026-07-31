using FluentValidation;
using NewsPortalCMS.Application.DTOs.Permission;

namespace NewsPortalCMS.Application.Validators;

public class UpdatePermissionValidator : AbstractValidator<UpdatePermissionDto>
{
    public UpdatePermissionValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Permission Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Permission name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Permission code is required.")
            .MaximumLength(100);

        RuleFor(x => x.Module)
            .NotEmpty().WithMessage("Module is required.")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}