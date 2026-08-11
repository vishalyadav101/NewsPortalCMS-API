using System;

namespace NewsPortalCMS.Application.DTOs.Advertisement
{
    public class AdvertisementResponseDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string BannerUrl { get; set; } = string.Empty;

        public string? RedirectUrl { get; set; }

        public int Position { get; set; }

        public string PositionName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}