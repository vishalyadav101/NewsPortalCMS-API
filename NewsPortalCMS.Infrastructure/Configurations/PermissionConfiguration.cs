using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Infrastructure.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Code)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(x => x.Code)
               .IsUnique();

        builder.Property(x => x.Module)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Description)
               .HasMaxLength(500);
    }
}