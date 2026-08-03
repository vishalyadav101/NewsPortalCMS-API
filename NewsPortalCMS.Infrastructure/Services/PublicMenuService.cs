using AutoMapper;
using NewsPortalCMS.Application.DTOs.Public;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Application.Services
{
    public class PublicMenuService : IPublicMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public PublicMenuService(
            IMenuRepository menuRepository,
            IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PublicMenuDto>> GetActiveMenusAsync()
        {
            var menus = await _menuRepository.GetActiveMenusAsync();

            var result = _mapper.Map<List<PublicMenuDto>>(menus);

            foreach (var menu in result)
            {
                menu.MenuItems = menu.MenuItems
                    .Where(x => x.ParentId == null)
                    .OrderBy(x => x.DisplayOrder)
                    .ToList();
            }

            return result;
        }

        public async Task<PublicMenuDto?> GetMenuByLocationAsync(string location)
        {
            var menu = await _menuRepository.GetMenuByLocationAsync(location);

            if (menu == null)
                return null;

            var dto = _mapper.Map<PublicMenuDto>(menu);

            dto.MenuItems = dto.MenuItems
                .Where(x => x.ParentId == null)
                .OrderBy(x => x.DisplayOrder)
                .ToList();

            return dto;
        }
    }
}