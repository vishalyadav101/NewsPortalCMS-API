using NewsPortalCMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortalCMS.Application.Interfaces.Repositories
{
    public interface IAdvertisementRepository
    {
        Task<Advertisement?> GetByIdAsync(Guid id);

        Task<IEnumerable<Advertisement>> GetAllAsync();

        Task AddAsync(Advertisement advertisement);

        Task UpdateAsync(Advertisement advertisement);

        Task DeleteAsync(Advertisement advertisement);
    }
}