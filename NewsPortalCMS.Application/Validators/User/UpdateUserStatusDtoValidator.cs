using FluentValidation;
using NewsPortalCMS.Application.DTOs.User;

namespace NewsPortalCMS.Application.Validators.User;

public class UpdateUserStatusDtoValidator
    : AbstractValidator<UpdateUserStatusDto>
{
    public UpdateUserStatusDtoValidator()
    {
        RuleFor(x => x.IsActive)
            .NotNull();
    }
}