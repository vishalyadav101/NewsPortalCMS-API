using FluentValidation;
using NewsPortalCMS.Application.DTOs.AuditLog;

namespace NewsPortalCMS.Application.Validators.AuditLog
{
    public class UpdateAuditLogDtoValidator : AbstractValidator<UpdateAuditLogDto>
    {
        public UpdateAuditLogDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Audit Log ID is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .MaximumLength(100);

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name is required.")
                .MaximumLength(150);

            RuleFor(x => x.Action)
                .NotEmpty().WithMessage("Action is required.")
                .MaximumLength(100);

            RuleFor(x => x.Module)
                .NotEmpty().WithMessage("Module is required.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.IpAddress)
                .MaximumLength(50);

            RuleFor(x => x.Browser)
                .MaximumLength(250);
        }
    }
}