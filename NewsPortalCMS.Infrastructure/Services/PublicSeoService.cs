using NewsPortalCMS.Application.DTOs.Public.Seo;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Application.Services
{
    public class PublicSeoService : IPublicSeoService
    {
        private readonly IPublicSeoRepository _seoRepository;

        public PublicSeoService(IPublicSeoRepository seoRepository)
        {
            _seoRepository = seoRepository;
        }

        public async Task<PublicSeoDto?> GetSeoByPageNameAsync(string pageName)
        {
            var seo = await _seoRepository.GetByPageNameAsync(pageName);

            if (seo == null)
            {
                return null;
            }

            return new PublicSeoDto
            {
                PageName = seo.PageName,
                MetaTitle = seo.MetaTitle,
                MetaDescription = seo.MetaDescription,
                MetaKeywords = seo.MetaKeywords,
                CanonicalUrl = seo.CanonicalUrl,
                Robots = seo.Robots,
                OgTitle = seo.OgTitle,
                OgDescription = seo.OgDescription,
                OgImage = seo.OgImage,
                TwitterTitle = seo.TwitterTitle,
                TwitterDescription = seo.TwitterDescription,
                TwitterImage = seo.TwitterImage,
                SchemaMarkup = seo.SchemaMarkup
            };
        }
    }
}