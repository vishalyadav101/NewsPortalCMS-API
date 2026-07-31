using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories
{
    public class SeoRepository : ISeoRepository
    {
        private readonly ApplicationDbContext _context;

        public SeoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Seo>> GetAllAsync()
        {
            return await _context.Seos
                .OrderBy(x => x.PageName)
                .ToListAsync();
        }

        public async Task<Seo?> GetByIdAsync(int id)
        {
            return await _context.Seos
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Seo?> GetByPageNameAsync(string pageName)
        {
            return await _context.Seos
                .FirstOrDefaultAsync(x => x.PageName == pageName);
        }

        public async Task<Seo> CreateAsync(Seo seo)
        {
            await _context.Seos.AddAsync(seo);
            await _context.SaveChangesAsync();

            return seo;
        }

        public async Task<Seo?> UpdateAsync(Seo seo)
        {
            var existingSeo = await _context.Seos
                .FirstOrDefaultAsync(x => x.Id == seo.Id);

            if (existingSeo == null)
                return null;

            existingSeo.PageName = seo.PageName;
            existingSeo.MetaTitle = seo.MetaTitle;
            existingSeo.MetaDescription = seo.MetaDescription;
            existingSeo.MetaKeywords = seo.MetaKeywords;
            existingSeo.CanonicalUrl = seo.CanonicalUrl;
            existingSeo.Robots = seo.Robots;
            existingSeo.OgTitle = seo.OgTitle;
            existingSeo.OgDescription = seo.OgDescription;
            existingSeo.OgImage = seo.OgImage;
            existingSeo.TwitterTitle = seo.TwitterTitle;
            existingSeo.TwitterDescription = seo.TwitterDescription;
            existingSeo.TwitterImage = seo.TwitterImage;
            existingSeo.SchemaMarkup = seo.SchemaMarkup;
            existingSeo.IsActive = seo.IsActive;
            existingSeo.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingSeo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var seo = await _context.Seos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (seo == null)
                return false;

            _context.Seos.Remove(seo);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}