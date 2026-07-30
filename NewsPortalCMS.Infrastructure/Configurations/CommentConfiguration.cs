using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Infrastructure.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        // Primary Key
        builder.HasKey(c => c.Id);

        // Properties
        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.Email)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(c => c.Content)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(c => c.IsApproved)
               .HasDefaultValue(false);

        builder.Property(c => c.IsActive)
               .HasDefaultValue(true);

        builder.Property(c => c.CreatedDate)
               .HasDefaultValueSql("GETUTCDATE()");

        // Relationship: News (1) -> (Many) Comments
        builder.HasOne(c => c.News)
               .WithMany(n => n.Comments)
               .HasForeignKey(c => c.NewsId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}