using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Infrastructure.Configurations;

public class SubCategoryConfiguration
    : IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        // Primary Key
        builder.HasKey(x => x.Id);

        // Name
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Slug
        builder.Property(x => x.Slug)
            .IsRequired()
            .HasMaxLength(150);

        // Description
        builder.Property(x => x.Description)
            .HasMaxLength(500);

        // Category -> SubCategories relationship
        builder.HasOne(x => x.Category)
            .WithMany(x => x.SubCategories)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Same Category ke andar duplicate slug allow nahi hoga
        builder.HasIndex(x => new { x.CategoryId, x.Slug })
            .IsUnique();
    }
}