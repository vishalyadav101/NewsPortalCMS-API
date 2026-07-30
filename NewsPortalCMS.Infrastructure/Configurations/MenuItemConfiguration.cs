using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Infrastructure.Configurations;

public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable("MenuItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(x => x.Url)
               .IsRequired()
               .HasMaxLength(300);

        builder.Property(x => x.Icon)
               .HasMaxLength(100);

        builder.Property(x => x.Target)
               .HasMaxLength(20);

        builder.Property(x => x.DisplayOrder)
               .HasDefaultValue(0);

        builder.Property(x => x.IsActive)
               .HasDefaultValue(true);

        builder.Property(x => x.CreatedDate)
               .IsRequired();

        builder.Property(x => x.UpdatedDate);

        builder.HasOne(x => x.Menu)
               .WithMany(x => x.MenuItems)
               .HasForeignKey(x => x.MenuId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Parent)
               .WithMany(x => x.Children)
               .HasForeignKey(x => x.ParentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}