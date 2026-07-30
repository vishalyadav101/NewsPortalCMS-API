using System.Reflection.Emit;
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
    public DbSet<StaticPage> StaticPages { get; set; }
    public DbSet<Menu> Menus { get; set; }

    public DbSet<MenuItem> MenuItems { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        // Menu -> MenuItems
        builder.Entity<Menu>()
            .HasMany(m => m.MenuItems)
            .WithOne(mi => mi.Menu)
            .HasForeignKey(mi => mi.MenuId)
            .OnDelete(DeleteBehavior.Cascade);

        // MenuItem -> Parent MenuItem
        builder.Entity<MenuItem>()
            .HasOne(mi => mi.Parent)
            .WithMany(mi => mi.Children)
            .HasForeignKey(mi => mi.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Infrastructure project ki saari IEntityTypeConfiguration
        // classes automatically apply hongi.
        builder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly
        );
        builder.Entity<Menu>()
    .HasMany(x => x.MenuItems)
    .WithOne(x => x.Menu);
    }
}