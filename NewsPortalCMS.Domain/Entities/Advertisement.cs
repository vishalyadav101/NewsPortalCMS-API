using System;
using NewsPortalCMS.Domain.Enums;

namespace NewsPortalCMS.Domain.Entities
{
    public class Advertisement
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string BannerUrl { get; set; } = string.Empty;

        public string? RedirectUrl { get; set; }

        public AdvertisementPosition Position { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}