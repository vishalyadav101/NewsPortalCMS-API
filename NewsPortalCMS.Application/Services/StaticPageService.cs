using AutoMapper;
using NewsPortalCMS.Application.DTOs.StaticPage;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services;

public class StaticPageService : IStaticPageService
{
    private readonly IStaticPageRepository _repository;
    private readonly IMapper _mapper;


    public StaticPageService(
        IStaticPageRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<IEnumerable<StaticPageResponseDto>> GetAllAsync()
    {
        var pages = await _repository.GetAllAsync();

        return _mapper.Map<IEnumerable<StaticPageResponseDto>>(pages);
    }


    public async Task<StaticPageResponseDto?> GetByIdAsync(int id)
    {
        var page = await _repository.GetByIdAsync(id);

        if (page == null)
            return null;

        return _mapper.Map<StaticPageResponseDto>(page);
    }


    public async Task<StaticPageResponseDto?> GetBySlugAsync(string slug)
    {
        var page = await _repository.GetBySlugAsync(slug);

        if (page == null)
            return null;

        return _mapper.Map<StaticPageResponseDto>(page);
    }


    public async Task<StaticPageResponseDto> CreateAsync(
        CreateStaticPageDto dto)
    {
        var page = _mapper.Map<StaticPage>(dto);

        page.CreatedDate = DateTime.UtcNow;

        await _repository.AddAsync(page);

        return _mapper.Map<StaticPageResponseDto>(page);
    }


    public async Task<bool> UpdateAsync(
        int id,
        UpdateStaticPageDto dto)
    {
        var page = await _repository.GetByIdAsync(id);

        if (page == null)
            return false;


        _mapper.Map(dto, page);

        page.UpdatedDate = DateTime.UtcNow;


        await _repository.UpdateAsync(page);

        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var page = await _repository.GetByIdAsync(id);

        if (page == null)
            return false;


        await _repository.DeleteAsync(page);

        return true;
    }
}