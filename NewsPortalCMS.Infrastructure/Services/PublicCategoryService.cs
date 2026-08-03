using AutoMapper;
using NewsPortalCMS.Application.DTOs.Public;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Application.Services
{
    public class PublicCategoryService : IPublicCategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public PublicCategoryService(
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PublicCategoryDto>> GetActiveCategoriesAsync()
        {
            var categories = await _categoryRepository.GetActiveCategoriesAsync();

            return _mapper.Map<IEnumerable<PublicCategoryDto>>(categories);
        }

        public async Task<PublicCategoryDto?> GetCategoryBySlugAsync(string slug)
        {
            var category = await _categoryRepository.GetBySlugAsync(slug);

            if (category == null)
                return null;

            return _mapper.Map<PublicCategoryDto>(category);
        }
    }
}