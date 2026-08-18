namespace NewsPortalCMS.Application.DTOs.WebsiteSetting;

public class WebsiteSettingCreateDto
{
    // =========================
    // General Information
    // =========================

    public string WebsiteName { get; set; } = string.Empty;

    public string? WebsiteTagline { get; set; }

    public string? OrganizationName { get; set; }

    public string? WebsiteUrl { get; set; }

    public string? WebsiteDescription { get; set; }

    public string? DefaultLanguage { get; set; }

    public string? TimeZone { get; set; }

    public string? CopyrightText { get; set; }


    // =========================
    // Media URLs
    // =========================

    public string? LogoUrl { get; set; }

    public string? FaviconUrl { get; set; }


    // =========================
    // Media Library References
    // =========================

    public int? DefaultNewsImageMediaId { get; set; }

    public int? DefaultSocialImageMediaId { get; set; }


    // =========================
    // Website Branding
    // =========================

    public string? PrimaryColor { get; set; }

    public string? SecondaryColor { get; set; }


    // =========================
    // Contact Information
    // =========================

    public string? ContactEmail { get; set; }

    public string? EditorialEmail { get; set; }

    public string? AdvertisingEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string? WhatsAppNumber { get; set; }

    public string? OfficeAddress { get; set; }

    public string? GoogleMapsUrl { get; set; }


    // =========================
    // Social Media
    // =========================

    public string? FacebookUrl { get; set; }

    public string? InstagramUrl { get; set; }

    public string? YouTubeUrl { get; set; }

    public string? TwitterUrl { get; set; }

    public string? LinkedInUrl { get; set; }

    public string? TelegramUrl { get; set; }

    public string? WhatsAppChannelUrl { get; set; }


    // =========================
    // SEO Information
    // =========================

    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }

    public string? MetaKeywords { get; set; }

    public string? CanonicalUrl { get; set; }

    public string? GoogleSiteVerification { get; set; }


    // =========================
    // Footer
    // =========================

    public string? FooterText { get; set; }
}