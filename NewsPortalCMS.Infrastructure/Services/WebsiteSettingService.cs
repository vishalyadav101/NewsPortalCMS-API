using AutoMapper;
using NewsPortalCMS.Application.DTOs.WebsiteSetting;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services;

public class WebsiteSettingService : IWebsiteSettingService
{
    private readonly IWebsiteSettingRepository _repository;
    private readonly IMapper _mapper;


    public WebsiteSettingService(
        IWebsiteSettingRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<WebsiteSettingResponseDto?> GetAsync()
    {
        var setting =
            await _repository.GetAsync();

        if (setting == null)
        {
            return null;
        }

        return _mapper.Map<WebsiteSettingResponseDto>(
            setting);
    }


    public async Task<WebsiteSettingResponseDto?> GetByIdAsync(
        int id)
    {
        var setting =
            await _repository.GetByIdAsync(id);

        if (setting == null)
        {
            return null;
        }

        return _mapper.Map<WebsiteSettingResponseDto>(
            setting);
    }


    public async Task<WebsiteSettingResponseDto> CreateAsync(
        WebsiteSettingCreateDto model)
    {
        var existing =
            await _repository.GetAsync();

        if (existing != null)
        {
            throw new InvalidOperationException(
                "Website settings already exist.");
        }


        var entity =
            _mapper.Map<WebsiteSetting>(model);


        var result =
            await _repository.AddAsync(entity);


        return _mapper.Map<WebsiteSettingResponseDto>(
            result);
    }


    public async Task<bool> UpdateAsync(
        int id,
        WebsiteSettingUpdateDto model)
    {
        var setting =
            await _repository.GetByIdAsync(id);


        if (setting == null)
        {
            return false;
        }


        _mapper.Map(
            model,
            setting);


        setting.UpdatedDate =
            DateTime.UtcNow;


        await _repository.UpdateAsync(setting);


        return true;
    }


    public async Task<bool> DeleteAsync(
        int id)
    {
        return await _repository.DeleteAsync(id);
    }
}