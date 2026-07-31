using FluentValidation;
using NewsPortalCMS.Application.DTOs.Notification;

namespace NewsPortalCMS.Application.Validators.Notification
{
    public class UpdateNotificationValidator
        : AbstractValidator<UpdateNotificationDto>
    {
        public UpdateNotificationValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(100);


            RuleFor(x => x.Message)
                .NotEmpty()
                .WithMessage("Message is required");


            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Notification type is required");
        }
    }
}