using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
    private readonly IWebHostEnvironment _environment;

    private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

    private static readonly string[] LogoExtensions =
    {
        ".png",
        ".jpg",
        ".jpeg",
        ".webp"
    };

    private static readonly string[] FaviconExtensions =
    {
        ".ico",
        ".png"
    };


    public WebsiteSettingService(
        IWebsiteSettingRepository repository,
        IMapper mapper,
        IWebHostEnvironment environment)
    {
        _repository = repository;
        _mapper = mapper;
        _environment = environment;
    }


    // =========================
    // GET
    // =========================

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


    // =========================
    // GET BY ID
    // =========================

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


    // =========================
    // CREATE
    // =========================

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


    // =========================
    // UPDATE
    // =========================

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


    // =========================
    // DELETE
    // =========================

    public async Task<bool> DeleteAsync(
        int id)
    {
        return await _repository.DeleteAsync(id);
    }


    // =========================
    // UPLOAD LOGO
    // =========================

    public async Task<string?> UploadLogoAsync(
        int id,
        IFormFile file)
    {
        var setting =
            await _repository.GetByIdAsync(id);

        if (setting == null)
        {
            return null;
        }


        ValidateFile(
            file,
            LogoExtensions,
            "Logo");


        var uploadFolder =
            GetUploadFolder();


        var extension =
            Path.GetExtension(file.FileName)
                .ToLowerInvariant();


        var fileName =
            $"{Guid.NewGuid():N}{extension}";


        var filePath =
            Path.Combine(
                uploadFolder,
                fileName);


        await SaveFileAsync(
            file,
            filePath);


        // Delete old logo after new file is saved
        DeleteOldFile(setting.LogoUrl);


        var logoUrl =
            $"/uploads/website/{fileName}";


        setting.LogoUrl =
            logoUrl;

        setting.UpdatedDate =
            DateTime.UtcNow;


        await _repository.UpdateAsync(setting);


        return logoUrl;
    }


    // =========================
    // UPLOAD FAVICON
    // =========================

    public async Task<string?> UploadFaviconAsync(
        int id,
        IFormFile file)
    {
        var setting =
            await _repository.GetByIdAsync(id);

        if (setting == null)
        {
            return null;
        }


        ValidateFile(
            file,
            FaviconExtensions,
            "Favicon");


        var uploadFolder =
            GetUploadFolder();


        var extension =
            Path.GetExtension(file.FileName)
                .ToLowerInvariant();


        var fileName =
            $"{Guid.NewGuid():N}{extension}";


        var filePath =
            Path.Combine(
                uploadFolder,
                fileName);


        await SaveFileAsync(
            file,
            filePath);


        // Delete old favicon after new file is saved
        DeleteOldFile(setting.FaviconUrl);


        var faviconUrl =
            $"/uploads/website/{fileName}";


        setting.FaviconUrl =
            faviconUrl;

        setting.UpdatedDate =
            DateTime.UtcNow;


        await _repository.UpdateAsync(setting);


        return faviconUrl;
    }


    // =========================
    // GET UPLOAD FOLDER
    // =========================

    private string GetUploadFolder()
    {
        var webRootPath =
            _environment.WebRootPath;


        if (string.IsNullOrWhiteSpace(webRootPath))
        {
            webRootPath =
                Path.Combine(
                    _environment.ContentRootPath,
                    "wwwroot");
        }


        var uploadFolder =
            Path.Combine(
                webRootPath,
                "uploads",
                "website");


        Directory.CreateDirectory(
            uploadFolder);


        return uploadFolder;
    }


    // =========================
    // VALIDATE FILE
    // =========================

    private static void ValidateFile(
        IFormFile file,
        string[] allowedExtensions,
        string fileType)
    {
        if (file == null ||
            file.Length == 0)
        {
            throw new InvalidOperationException(
                $"{fileType} file is required.");
        }


        if (file.Length > MaxFileSize)
        {
            throw new InvalidOperationException(
                $"{fileType} size cannot exceed 5 MB.");
        }


        var extension =
            Path.GetExtension(file.FileName)
                .ToLowerInvariant();


        if (!allowedExtensions.Contains(
                extension))
        {
            throw new InvalidOperationException(
                $"Invalid {fileType} format. " +
                $"Allowed formats: {string.Join(", ", allowedExtensions)}.");
        }
    }


    // =========================
    // SAVE FILE
    // =========================

    private static async Task SaveFileAsync(
        IFormFile file,
        string filePath)
    {
        await using var stream =
            new FileStream(
                filePath,
                FileMode.Create);


        await file.CopyToAsync(stream);
    }


    // =========================
    // DELETE OLD FILE
    // =========================

    private void DeleteOldFile(
        string? fileUrl)
    {
        if (string.IsNullOrWhiteSpace(fileUrl))
        {
            return;
        }


        var relativePath =
            fileUrl
                .TrimStart('/')
                .Replace(
                    '/',
                    Path.DirectorySeparatorChar);


        var webRootPath =
            _environment.WebRootPath;


        if (string.IsNullOrWhiteSpace(webRootPath))
        {
            webRootPath =
                Path.Combine(
                    _environment.ContentRootPath,
                    "wwwroot");
        }


        var oldFilePath =
            Path.Combine(
                webRootPath,
                relativePath);


        if (File.Exists(oldFilePath))
        {
            File.Delete(oldFilePath);
        }
    }
}