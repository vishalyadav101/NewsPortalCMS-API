using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories;

public class MediaRepository : IMediaRepository
{
    private readonly ApplicationDbContext _context;

    public MediaRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Media> AddAsync(Media media)
    {
        await _context.Media.AddAsync(media);
        await _context.SaveChangesAsync();

        return media;
    }


    public async Task<IEnumerable<Media>> GetAllAsync()
    {
        return await _context.Media
            .OrderByDescending(x => x.UploadedDate)
            .ToListAsync();
    }


    public async Task<Media?> GetByIdAsync(int id)
    {
        return await _context.Media
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task UpdateAsync(Media media)
    {
        _context.Media.Update(media);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(Media media)
    {
        _context.Media.Remove(media);
        await _context.SaveChangesAsync();
    }
}