using System;

namespace NewsPortalCMS.Application.DTOs.Advertisement
{
    public class CreateAdvertisementDto
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Existing Media Library reference
        public int MediaId { get; set; }

        public string? RedirectUrl { get; set; }

        public int Position { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public int DisplayOrder { get; set; }
    }
}