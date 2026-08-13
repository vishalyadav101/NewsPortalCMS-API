using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPortalCMS.Entities;

namespace NewsPortalCMS.Infrastructure.Data.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            // Performance Indexes

            // Filter by deleted status
            builder.HasIndex(n => n.IsDeleted);

            // Filter published news
            builder.HasIndex(n => n.IsPublished);

            // Filter featured news
            builder.HasIndex(n => n.IsFeatured);

            // Sort by publish date
            builder.HasIndex(n => n.PublishDate);

            // Sort by popularity
            builder.HasIndex(n => n.ViewCount);

            // Search / lookup by SEO slug
            builder.HasIndex(n => n.Slug);
        }
    }
}