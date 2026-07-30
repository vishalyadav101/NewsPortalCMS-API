using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortalCMS.Infrastructure.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly ApplicationDbContext _context;

        public AdvertisementRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Advertisement advertisement)
        {
            await _context.Advertisements.AddAsync(advertisement);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Advertisement advertisement)
        {
            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Advertisement>> GetAllAsync()
        {
            return await _context.Advertisements
                .Include(x => x.Media)
                .ToListAsync();
        }


        public async Task<Advertisement?> GetByIdAsync(Guid id)
        {
            return await _context.Advertisements
                .Include(x => x.Media)
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task UpdateAsync(Advertisement advertisement)
        {
            _context.Advertisements.Update(advertisement);
            await _context.SaveChangesAsync();
        }
    }
}