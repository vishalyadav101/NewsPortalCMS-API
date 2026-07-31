using FluentValidation;
using NewsPortalCMS.Application.DTOs.Notification;

namespace NewsPortalCMS.Application.Validators.Notification
{
    public class CreateNotificationValidator
        : AbstractValidator<CreateNotificationDto>
    {
        public CreateNotificationValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(100)
                .WithMessage("Title cannot exceed 100 characters");


            RuleFor(x => x.Message)
                .NotEmpty()
                .WithMessage("Message is required");


            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Notification type is required");
        }
    }
}