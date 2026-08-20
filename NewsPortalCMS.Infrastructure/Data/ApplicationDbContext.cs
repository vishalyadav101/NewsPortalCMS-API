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

    // ============================================================
    // CATEGORY
    // ============================================================

    public DbSet<Category> Categories { get; set; }

    // ============================================================
    // SUB CATEGORY
    // ============================================================

    public DbSet<SubCategory> SubCategories { get; set; }

    // ============================================================
    // TAG
    // ============================================================

    public DbSet<Tag> Tags { get; set; }

    // ============================================================
    // NEWS
    // ============================================================

    public DbSet<News> News { get; set; }

    public DbSet<NewsTag> NewsTags { get; set; }

    // ============================================================
    // MEDIA
    // ============================================================

    public DbSet<Media> Media { get; set; }

    // ============================================================
    // STATIC PAGES
    // ============================================================

    public DbSet<StaticPage> StaticPages { get; set; }

    // ============================================================
    // MENUS
    // ============================================================

    public DbSet<Menu> Menus { get; set; }

    public DbSet<MenuItem> MenuItems { get; set; }

    // ============================================================
    // COMMENTS
    // ============================================================

    public DbSet<Comment> Comments { get; set; }

    // ============================================================
    // ADVERTISEMENTS
    // ============================================================

    public DbSet<Advertisement> Advertisements { get; set; }

    // ============================================================
    // PERMISSIONS
    // ============================================================

    public DbSet<Permission> Permissions { get; set; }

    public DbSet<RolePermission> RolePermissions { get; set; }

    // ============================================================
    // SEO
    // ============================================================

    public DbSet<Seo> Seos { get; set; }

    // ============================================================
    // WEBSITE SETTINGS
    // ============================================================

    public DbSet<WebsiteSetting> WebsiteSettings { get; set; }

    // ============================================================
    // NOTIFICATIONS
    // ============================================================

    public DbSet<Notification> Notifications { get; set; }

    // ============================================================
    // AUDIT LOGS
    // ============================================================

    public DbSet<AuditLog> AuditLogs { get; set; }

    // ============================================================
    // MODEL CREATING
    // ============================================================

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ========================================================
        // NEWS -> CATEGORY
        // ========================================================

        builder.Entity<News>()
            .HasOne(n => n.Category)
            .WithMany()
            .HasForeignKey(n => n.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // ========================================================
        // NEWS -> SUB CATEGORY
        // ========================================================

        builder.Entity<News>()
            .HasOne(n => n.SubCategory)
            .WithMany()
            .HasForeignKey(n => n.SubCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // ========================================================
        // MENU -> MENU ITEMS
        // ========================================================

        builder.Entity<Menu>()
            .HasMany(m => m.MenuItems)
            .WithOne(mi => mi.Menu)
            .HasForeignKey(mi => mi.MenuId)
            .OnDelete(DeleteBehavior.Cascade);

        // ========================================================
        // MENU ITEM -> PARENT MENU ITEM
        // ========================================================

        builder.Entity<MenuItem>()
            .HasOne(mi => mi.Parent)
            .WithMany(mi => mi.Children)
            .HasForeignKey(mi => mi.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        // ========================================================
        // APPLY ENTITY CONFIGURATIONS
        // ========================================================

        builder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly
        );
    }
}