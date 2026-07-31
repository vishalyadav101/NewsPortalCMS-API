using FluentValidation;
using NewsPortalCMS.Application.DTOs.Permission;

namespace NewsPortalCMS.Application.Validators;

public class CreatePermissionValidator : AbstractValidator<CreatePermissionDto>
{
    public CreatePermissionValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Permission name is required.")
            .MaximumLength(100).WithMessage("Permission name cannot exceed 100 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Permission code is required.")
            .MaximumLength(100).WithMessage("Permission code cannot exceed 100 characters.");

        RuleFor(x => x.Module)
            .NotEmpty().WithMessage("Module is required.")
            .MaximumLength(100).WithMessage("Module cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}