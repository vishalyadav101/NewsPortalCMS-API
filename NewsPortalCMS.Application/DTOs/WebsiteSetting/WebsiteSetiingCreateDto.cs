namespace NewsPortalCMS.Application.DTOs.WebsiteSetting;

public class WebsiteSettingCreateDto
{
    public string WebsiteName { get; set; } = string.Empty;

    public string? LogoUrl { get; set; }

    public string? FaviconUrl { get; set; }

    public string? WebsiteDescription { get; set; }


    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string? Address { get; set; }


    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }

    public string? MetaKeywords { get; set; }


    public string? FacebookUrl { get; set; }

    public string? TwitterUrl { get; set; }

    public string? InstagramUrl { get; set; }

    public string? YouTubeUrl { get; set; }

    public string? LinkedInUrl { get; set; }


    public string? FooterText { get; set; }
}