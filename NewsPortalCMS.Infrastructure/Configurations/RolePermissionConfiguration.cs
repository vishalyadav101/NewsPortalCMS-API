using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Infrastructure.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Permission)
               .WithMany(x => x.RolePermissions)
               .HasForeignKey(x => x.PermissionId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Role)
               .WithMany()
               .HasForeignKey(x => x.RoleId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}