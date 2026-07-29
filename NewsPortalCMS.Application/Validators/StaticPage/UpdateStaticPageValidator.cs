using FluentValidation;
using NewsPortalCMS.Application.DTOs.StaticPage;

namespace NewsPortalCMS.Application.Validators.StaticPage;

public class UpdateStaticPageValidator : AbstractValidator<UpdateStaticPageDto>
{
    public UpdateStaticPageValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Content)
            .NotEmpty();

        RuleFor(x => x.MetaTitle)
            .MaximumLength(200);

        RuleFor(x => x.MetaDescription)
            .MaximumLength(500);

        RuleFor(x => x.MetaKeywords)
            .MaximumLength(500);
    }
}