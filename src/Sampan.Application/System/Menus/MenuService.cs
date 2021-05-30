using System.Collections.Generic;
using System.Threading.Tasks;
using Sampan.Domain.System;
using Sampan.Infrastructure.Repository;
using Sampan.Public.Dto;
using Sampan.Service.Contract.System.Menus;

namespace Sampan.Application.System.Menus
{
    public class MenuService : Service, IMenuService
    {
        private readonly MenuManager _menuManager;
        private readonly IRepository<Menu> _repository;

        public MenuService(MenuManager menuManager,
            IRepository<Menu> repository)

        {
            _menuManager = menuManager;
            _repository = repository;
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<MenuListDto>> GetAsync()
        {
            var menus = await _repository
                .Include(a => a.CreateUser)
                .Include(a => a.Permission)
                .OrderBy(a => a.Sort)
                .ToTreeListAsync();
            var menuDtos = Mapper.Map<List<MenuListDto>>(menus);
            return menuDtos;
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MenuDto> GetAsync(int id)
        {
            var menu = await _repository.GetAsync(id);
            return Mapper.Map<MenuDto>(menu);
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input"></param>
        public async Task CreateAsync(CreateMenuDto input)
        {
            var menu = Mapper.Map<Menu>(input);
            menu.IsEnable = true;
            await _repository.InsertAsync(menu);
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        public async Task UpdateAsync(int id, UpdateMenuDto input)
        {
            var menu = await _repository.GetAsync(id);
            Mapper.Map(input, menu);
            await _repository.UpdateAsync(menu);
        }

        /// <summary>
        /// 设置启用/禁用状态
        /// </summary>
        /// <param name="input"></param>
        public async Task SetIsEnableAsync(SetEnableDto input)
        {
            var menu = await _repository.GetAsync(input.Id);
            menu.IsEnable = input.IsEnable;

            await _repository.UpdateAsync(menu);
        }

        /// <summary>
        /// 绑定权限
        /// </summary>
        /// <param name="input"></param>
        public async Task BindPermissionAsync(BindPermissionDto input)
        {
            var menu = await _repository.GetAsync(input.Id);
            menu.PermissionId = input.PermissionId;

            await _repository.UpdateAsync(menu);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}