using System.ComponentModel.DataAnnotations;

namespace NewsPortalCMS.Application.DTOs.NewsTag
{
    public class AssignNewsTagsDto
    {
        [Required]
        public int NewsId { get; set; }

        [Required]
        public List<int> TagIds { get; set; } = new();
    }
}