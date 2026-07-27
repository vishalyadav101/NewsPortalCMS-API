using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Primary Key
        builder.HasKey(x => x.Id);

        // Category name required hai
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Slug required hai
        builder.Property(x => x.Slug)
            .IsRequired()
            .HasMaxLength(150);

        // Database level par duplicate slug allow nahi hoga
        builder.HasIndex(x => x.Slug)
            .IsUnique();

        // Description optional hai
        builder.Property(x => x.Description)
            .HasMaxLength(500);
    }
}