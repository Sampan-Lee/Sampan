using System.Threading.Tasks;
using Sampan.Domain.System;
using Sampan.Infrastructure.Repository;
using Sampan.Service.Contract.System.Menus;

namespace Sampan.Application.System.Menus
{
    public class MenuService :
        CrudService<Menu, MenuDto, MenuListDto, GetMenuListDto, CreateMenuDto, UpdateMenuDto>,
        IMenuService
    {
        private readonly MenuManager _menuManager;

        public MenuService(MenuManager menuManager,
            IRepository<Menu> repository)
            : base(repository)
        {
            _menuManager = menuManager;
        }

        public override async Task CreateAsync(CreateMenuDto input)
        {
            var menu = await _menuManager.CreateAsync(
                input.ParentId,
                input.PermissionId,
                input.Name,
                input.Icon,
                input.Path
            );

            await Repository.InsertAsync(menu);
        }
    }
}