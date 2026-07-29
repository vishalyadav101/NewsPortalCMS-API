using NewsPortalCMS.Application.DTOs.Media;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services;

public class MediaService : IMediaService
{
    private readonly IMediaRepository _mediaRepository;

    public MediaService(IMediaRepository mediaRepository)
    {
        _mediaRepository = mediaRepository;
    }


    public async Task<MediaResponseDto> CreateAsync(CreateMediaDto dto)
    {
        var media = new Media
        {
            FileName = dto.FileName,
            OriginalFileName = dto.OriginalFileName,
            FilePath = dto.FilePath,
            FileType = dto.FileType,
            ContentType = dto.ContentType,
            FileSize = dto.FileSize,
            UploadedBy = dto.UploadedBy,
            UploadedDate = DateTime.UtcNow,
            IsActive = true
        };


        var result = await _mediaRepository.AddAsync(media);


        return new MediaResponseDto
        {
            Id = result.Id,
            FileName = result.FileName,
            OriginalFileName = result.OriginalFileName,
            FilePath = result.FilePath,
            FileType = result.FileType,
            ContentType = result.ContentType,
            FileSize = result.FileSize,
            UploadedBy = result.UploadedBy,
            UploadedDate = result.UploadedDate,
            IsActive = result.IsActive
        };
    }


    public async Task<IEnumerable<MediaResponseDto>> GetAllAsync()
    {
        var mediaList = await _mediaRepository.GetAllAsync();


        return mediaList.Select(media => new MediaResponseDto
        {
            Id = media.Id,
            FileName = media.FileName,
            OriginalFileName = media.OriginalFileName,
            FilePath = media.FilePath,
            FileType = media.FileType,
            ContentType = media.ContentType,
            FileSize = media.FileSize,
            UploadedBy = media.UploadedBy,
            UploadedDate = media.UploadedDate,
            IsActive = media.IsActive
        });
    }


    public async Task<MediaResponseDto?> GetByIdAsync(int id)
    {
        var media = await _mediaRepository.GetByIdAsync(id);


        if (media == null)
            return null;


        return new MediaResponseDto
        {
            Id = media.Id,
            FileName = media.FileName,
            OriginalFileName = media.OriginalFileName,
            FilePath = media.FilePath,
            FileType = media.FileType,
            ContentType = media.ContentType,
            FileSize = media.FileSize,
            UploadedBy = media.UploadedBy,
            UploadedDate = media.UploadedDate,
            IsActive = media.IsActive
        };
    }


    public async Task<bool> UpdateAsync(UpdateMediaDto dto)
    {
        var media = await _mediaRepository.GetByIdAsync(dto.Id);


        if (media == null)
            return false;


        media.FileName = dto.FileName;
        media.OriginalFileName = dto.OriginalFileName;
        media.FilePath = dto.FilePath;
        media.FileType = dto.FileType;
        media.ContentType = dto.ContentType;
        media.FileSize = dto.FileSize;
        media.UploadedBy = dto.UploadedBy;
        media.IsActive = dto.IsActive;


        await _mediaRepository.UpdateAsync(media);


        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var media = await _mediaRepository.GetByIdAsync(id);


        if (media == null)
            return false;


        await _mediaRepository.DeleteAsync(media);


        return true;
    }
}