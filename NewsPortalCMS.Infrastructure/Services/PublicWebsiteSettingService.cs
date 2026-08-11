using AutoMapper;
using NewsPortalCMS.Application.DTOs.PublicWebsiteSetting;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Application.Services;

public class PublicWebsiteSettingService : IPublicWebsiteSettingService
{
    private readonly IWebsiteSettingRepository _repository;
    private readonly IMapper _mapper;

    public PublicWebsiteSettingService(
        IWebsiteSettingRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PublicWebsiteSettingResponseDto?> GetAsync()
    {
        var setting = await _repository.GetAsync();

        if (setting == null)
        {
            return null;
        }

        return _mapper.Map<PublicWebsiteSettingResponseDto>(setting);
    }
}