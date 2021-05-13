namespace Sampan.Service.Contract.System.Menus
{
    /// <summary>
    /// 菜单服务接口
    /// </summary>
    public interface IMenuService : ICrudService<MenuDto, MenuListDto, GetMenuListDto, CreateMenuDto, UpdateMenuDto>
    {
    }
}