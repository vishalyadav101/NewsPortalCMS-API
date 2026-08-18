using AutoMapper;
using Microsoft.AspNetCore.Http;
using NewsPortalCMS.Application.DTOs.WebsiteSetting;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services;

public class WebsiteSettingService : IWebsiteSettingService
{
    private readonly IWebsiteSettingRepository _repository;
    private readonly IMapper _mapper;
    private readonly IWebsiteSettingFileService _fileService;

    public WebsiteSettingService(
        IWebsiteSettingRepository repository,
        IMapper mapper,
        IWebsiteSettingFileService fileService)
    {
        _repository = repository;
        _mapper = mapper;
        _fileService = fileService;
    }

    // =========================================================
    // GET
    // =========================================================

    public async Task<WebsiteSettingResponseDto?> GetAsync()
    {
        var entity = await _repository.GetAsync();

        if (entity == null)
            return null;

        return _mapper.Map<WebsiteSettingResponseDto>(entity);
    }

    // =========================================================
    // GET BY ID
    // =========================================================

    public async Task<WebsiteSettingResponseDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        return _mapper.Map<WebsiteSettingResponseDto>(entity);
    }

    // =========================================================
    // CREATE
    // =========================================================

    public async Task<WebsiteSettingResponseDto> CreateAsync(
        WebsiteSettingCreateDto model)
    {
        var existing = await _repository.GetAsync();

        if (existing != null)
        {
            throw new InvalidOperationException(
                "Website setting already exists.");
        }

        var entity = _mapper.Map<WebsiteSetting>(model);

        entity.CreatedDate = DateTime.UtcNow;

        var result = await _repository.AddAsync(entity);

        return _mapper.Map<WebsiteSettingResponseDto>(result);
    }

    // =========================================================
    // UPDATE
    // =========================================================

    public async Task<bool> UpdateAsync(
        int id,
        WebsiteSettingUpdateDto model)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return false;

        _mapper.Map(model, entity);

        entity.UpdatedDate = DateTime.UtcNow;

        await _repository.UpdateAsync(entity);

        return true;
    }

    // =========================================================
    // DELETE
    // =========================================================

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return false;

        // Delete logo file
        if (!string.IsNullOrWhiteSpace(entity.LogoUrl))
        {
            _fileService.DeleteFile(entity.LogoUrl);
        }

        // Delete favicon file
        if (!string.IsNullOrWhiteSpace(entity.FaviconUrl))
        {
            _fileService.DeleteFile(entity.FaviconUrl);
        }

        await _repository.DeleteAsync(entity);

        return true;
    }

    // =========================================================
    // UPLOAD LOGO
    // =========================================================

    public async Task<string?> UploadLogoAsync(
        int id,
        IFormFile file)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        if (file == null || file.Length == 0)
        {
            throw new InvalidOperationException(
                "Logo file is required.");
        }

        // Delete old logo
        if (!string.IsNullOrWhiteSpace(entity.LogoUrl))
        {
            _fileService.DeleteFile(entity.LogoUrl);
        }

        // Upload new logo
        var logoUrl = await _fileService.UploadLogoAsync(file);

        entity.LogoUrl = logoUrl;
        entity.UpdatedDate = DateTime.UtcNow;

        await _repository.UpdateAsync(entity);

        return logoUrl;
    }

    // =========================================================
    // UPLOAD FAVICON
    // =========================================================

    public async Task<string?> UploadFaviconAsync(
        int id,
        IFormFile file)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        if (file == null || file.Length == 0)
        {
            throw new InvalidOperationException(
                "Favicon file is required.");
        }

        // Delete old favicon
        if (!string.IsNullOrWhiteSpace(entity.FaviconUrl))
        {
            _fileService.DeleteFile(entity.FaviconUrl);
        }

        // Upload new favicon
        var faviconUrl =
            await _fileService.UploadFaviconAsync(file);

        entity.FaviconUrl = faviconUrl;
        entity.UpdatedDate = DateTime.UtcNow;

        await _repository.UpdateAsync(entity);

        return faviconUrl;
    }
}