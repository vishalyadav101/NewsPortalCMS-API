using AutoMapper;
using NewsPortalCMS.Application.DTOs.Advertisement;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NewsPortalCMS.Application.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _repository;
        private readonly IMapper _mapper;

        public AdvertisementService(
            IAdvertisementRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AdvertisementResponseDto> CreateAsync(
            CreateAdvertisementDto dto)
        {
            var advertisement = new Advertisement
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                RedirectUrl = dto.RedirectUrl,
                Position = (NewsPortalCMS.Domain.Enums.AdvertisementPosition)dto.Position,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = dto.IsActive,
                DisplayOrder = dto.DisplayOrder,
                CreatedDate = DateTime.UtcNow
            };

            // Upload Banner
            if (dto.BannerFile != null)
            {
                advertisement.BannerUrl =
                    await SaveBannerAsync(dto.BannerFile);
            }

            await _repository.AddAsync(advertisement);

            return _mapper.Map<AdvertisementResponseDto>(advertisement);
        }

        public async Task<IEnumerable<AdvertisementResponseDto>> GetAllAsync()
        {
            var advertisements = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<AdvertisementResponseDto>>(
                advertisements
            );
        }

        public async Task<AdvertisementResponseDto?> GetByIdAsync(Guid id)
        {
            var advertisement = await _repository.GetByIdAsync(id);

            if (advertisement == null)
                return null;

            return _mapper.Map<AdvertisementResponseDto>(advertisement);
        }

        public async Task<bool> UpdateAsync(
            Guid id,
            UpdateAdvertisementDto dto)
        {
            var advertisement = await _repository.GetByIdAsync(id);

            if (advertisement == null)
                return false;

            advertisement.Title = dto.Title;
            advertisement.Description = dto.Description;
            advertisement.RedirectUrl = dto.RedirectUrl;
            advertisement.Position =
                (NewsPortalCMS.Domain.Enums.AdvertisementPosition)dto.Position;
            advertisement.StartDate = dto.StartDate;
            advertisement.EndDate = dto.EndDate;
            advertisement.IsActive = dto.IsActive;
            advertisement.DisplayOrder = dto.DisplayOrder;
            advertisement.UpdatedDate = DateTime.UtcNow;

            // Replace banner only when a new file is selected
            if (dto.BannerFile != null)
            {
                advertisement.BannerUrl =
                    await SaveBannerAsync(dto.BannerFile);
            }

            await _repository.UpdateAsync(advertisement);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var advertisement = await _repository.GetByIdAsync(id);

            if (advertisement == null)
                return false;

            await _repository.DeleteAsync(advertisement);

            return true;
        }

        private async Task<string> SaveBannerAsync(
            Microsoft.AspNetCore.Http.IFormFile file)
        {
            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "advertisements"
            );

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var extension = Path.GetExtension(file.FileName);

            var fileName =
                $"{Guid.NewGuid()}{extension}";

            var filePath =
                Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(
                filePath,
                FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/advertisements/{fileName}";
        }
    }
}