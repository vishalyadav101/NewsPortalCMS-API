using AutoMapper;
using NewsPortalCMS.Application.DTOs.Public;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Application.Services;

public class PublicStaticPageService : IPublicStaticPageService
{
    private readonly IStaticPageRepository _staticPageRepository;
    private readonly IMapper _mapper;

    public PublicStaticPageService(
        IStaticPageRepository staticPageRepository,
        IMapper mapper)
    {
        _staticPageRepository = staticPageRepository;
        _mapper = mapper;
    }


    public async Task<IEnumerable<PublicStaticPageDto>> GetActivePagesAsync()
    {
        var pages = await _staticPageRepository.GetActivePagesAsync();

        return _mapper.Map<IEnumerable<PublicStaticPageDto>>(pages);
    }


    public async Task<PublicStaticPageDto?> GetPageBySlugAsync(string slug)
    {
        var page = await _staticPageRepository.GetActiveBySlugAsync(slug);

        if (page == null)
            return null;

        return _mapper.Map<PublicStaticPageDto>(page);
    }
}