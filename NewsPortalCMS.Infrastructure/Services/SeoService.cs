using AutoMapper;
using NewsPortalCMS.Application.DTOs.Seo;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services
{
    public class SeoService : ISeoService
    {
        private readonly ISeoRepository _seoRepository;
        private readonly IMapper _mapper;

        public SeoService(ISeoRepository seoRepository, IMapper mapper)
        {
            _seoRepository = seoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SeoResponseDto>> GetAllAsync()
        {
            var seoList = await _seoRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<SeoResponseDto>>(seoList);
        }

        public async Task<SeoResponseDto?> GetByIdAsync(int id)
        {
            var seo = await _seoRepository.GetByIdAsync(id);

            if (seo == null)
                return null;

            return _mapper.Map<SeoResponseDto>(seo);
        }

        public async Task<SeoResponseDto?> GetByPageNameAsync(string pageName)
        {
            var seo = await _seoRepository.GetByPageNameAsync(pageName);

            if (seo == null)
                return null;

            return _mapper.Map<SeoResponseDto>(seo);
        }

        public async Task<SeoResponseDto> CreateAsync(CreateSeoDto createSeoDto)
        {
            var seo = _mapper.Map<Seo>(createSeoDto);

            var createdSeo = await _seoRepository.CreateAsync(seo);

            return _mapper.Map<SeoResponseDto>(createdSeo);
        }

        public async Task<SeoResponseDto?> UpdateAsync(UpdateSeoDto updateSeoDto)
        {
            var seo = _mapper.Map<Seo>(updateSeoDto);

            var updatedSeo = await _seoRepository.UpdateAsync(seo);

            if (updatedSeo == null)
                return null;

            return _mapper.Map<SeoResponseDto>(updatedSeo);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _seoRepository.DeleteAsync(id);
        }
    }
}