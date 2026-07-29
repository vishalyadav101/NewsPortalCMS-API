using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Domain.Entities;
 
 
using NewsPortalCMS.Entities;
 

namespace NewsPortalCMS.Infrastructure.Data;

public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Category
    public DbSet<Category> Categories { get; set; }

    // SubCategory
    public DbSet<SubCategory> SubCategories { get; set; }


    // Tag
    public DbSet<Tag> Tags { get; set; }

    public DbSet<News> News { get; set; }
    public DbSet<NewsTag> NewsTags { get; set; }

    public DbSet<Media> Media { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Infrastructure project ki saari IEntityTypeConfiguration
        // classes automatically apply hongi.
        builder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly
        );
    }
}