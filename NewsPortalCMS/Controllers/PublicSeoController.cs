using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.API.Controllers.Public
{
    [Route("api/public/seo")]
    [ApiController]
    public class PublicSeoController : ControllerBase
    {
        private readonly IPublicSeoService _seoService;

        public PublicSeoController(IPublicSeoService seoService)
        {
            _seoService = seoService;
        }

        [HttpGet("{pageName}")]
        public async Task<IActionResult> GetSeo(string pageName)
        {
            var seo = await _seoService.GetSeoByPageNameAsync(pageName);

            if (seo == null)
            {
                return NotFound(new
                {
                    message = "SEO data not found"
                });
            }

            return Ok(seo);
        }
    }
}