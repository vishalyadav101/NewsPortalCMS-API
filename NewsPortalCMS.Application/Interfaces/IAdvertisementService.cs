using NewsPortalCMS.Application.DTOs.Advertisement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface IAdvertisementService
    {
        Task<AdvertisementResponseDto> CreateAsync(CreateAdvertisementDto dto);

        Task<AdvertisementResponseDto?> GetByIdAsync(Guid id);

        Task<IEnumerable<AdvertisementResponseDto>> GetAllAsync();

        Task<bool> UpdateAsync(Guid id, UpdateAdvertisementDto dto);

        Task<bool> DeleteAsync(Guid id);
    }
}