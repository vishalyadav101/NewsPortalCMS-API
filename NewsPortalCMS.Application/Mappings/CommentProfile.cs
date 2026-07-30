using AutoMapper;
using NewsPortalCMS.Application.DTOs.Comment;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Mappings;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        // Entity -> Response DTO
        CreateMap<Comment, CommentResponseDto>();

        // Create DTO -> Entity
        CreateMap<CreateCommentDto, Comment>();

        // Update DTO -> Entity
        CreateMap<UpdateCommentDto, Comment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.NewsId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
    }
}