namespace NewsPortalCMS.Domain.Entities;

public class WebsiteSetting
{
    public int Id { get; set; }

    // General Information

    public string WebsiteName { get; set; } = string.Empty;

    public string? LogoUrl { get; set; }

    public string? FaviconUrl { get; set; }

    public string? WebsiteDescription { get; set; }


    // Contact Information

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string? Address { get; set; }


    // SEO Information

    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }

    public string? MetaKeywords { get; set; }


    // Social Media

    public string? FacebookUrl { get; set; }

    public string? TwitterUrl { get; set; }

    public string? InstagramUrl { get; set; }

    public string? YouTubeUrl { get; set; }

    public string? LinkedInUrl { get; set; }


    // Footer

    public string? FooterText { get; set; }


    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedDate { get; set; }
}