using FluentValidation;
using NewsPortalCMS.Application.DTOs.Seo;

namespace NewsPortalCMS.Application.Validators
{
    public class UpdateSeoValidator : AbstractValidator<UpdateSeoDto>
    {
        public UpdateSeoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.PageName)
                .NotEmpty()
                .WithMessage("Page Name is required.")
                .MaximumLength(200);

            RuleFor(x => x.MetaTitle)
                .NotEmpty()
                .WithMessage("Meta Title is required.")
                .MaximumLength(250);

            RuleFor(x => x.MetaDescription)
                .MaximumLength(500);

            RuleFor(x => x.MetaKeywords)
                .MaximumLength(500);

            RuleFor(x => x.CanonicalUrl)
                .MaximumLength(250);

            RuleFor(x => x.Robots)
                .MaximumLength(100);

            RuleFor(x => x.OgTitle)
                .MaximumLength(250);

            RuleFor(x => x.OgDescription)
                .MaximumLength(500);

            RuleFor(x => x.OgImage)
                .MaximumLength(250);

            RuleFor(x => x.TwitterTitle)
                .MaximumLength(250);

            RuleFor(x => x.TwitterDescription)
                .MaximumLength(500);

            RuleFor(x => x.TwitterImage)
                .MaximumLength(250);
        }
    }
}