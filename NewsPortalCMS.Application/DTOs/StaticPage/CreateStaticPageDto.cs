namespace NewsPortalCMS.Application.DTOs.StaticPage;

public class CreateStaticPageDto
{
    public string Title { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;


    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }

    public string? MetaKeywords { get; set; }


    public bool Status { get; set; }
}