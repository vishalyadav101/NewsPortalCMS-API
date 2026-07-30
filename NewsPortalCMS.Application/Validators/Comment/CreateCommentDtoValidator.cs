using FluentValidation;
using NewsPortalCMS.Application.DTOs.Comment;

namespace NewsPortalCMS.Application.Validators.Comment;

public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
{
    public CreateCommentDtoValidator()
    {
        RuleFor(x => x.NewsId)
            .NotEmpty()
            .WithMessage("News Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .MaximumLength(150)
            .WithMessage("Email cannot exceed 150 characters.");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Comment content is required.")
            .MaximumLength(1000)
            .WithMessage("Comment cannot exceed 1000 characters.");
    }
}