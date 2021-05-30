using System.Collections.Generic;
using System.Threading.Tasks;
using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.Menus
{
    /// <summary>
    /// 菜单服务接口
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        Task<List<MenuListDto>> GetAsync();

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MenuDto> GetAsync(int id);

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateMenuDto input);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(int id, UpdateMenuDto input);

        /// <summary>
        /// 设置启用/禁用状态
        /// </summary>
        /// <returns></returns>
        Task SetIsEnableAsync(SetEnableDto input);

        /// <summary>
        /// 绑定权限
        /// </summary>
        /// <returns></returns>
        Task BindPermissionAsync(BindPermissionDto input);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}