using System.Collections.Generic;
using System.Threading.Tasks;
using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.Roles
{
    /// <summary>
    /// 权限服务接口
    /// </summary>
    public interface IRoleService : ICrudService<RoleDto, RoleListDto, GetRoleListDto, CreateRoleDto, UpdateRoleDto>
    {
        /// <summary>
        /// 获取角色字典
        /// </summary>
        /// <returns></returns>
        Task<List<DropdownDto>> GetDictionaryAsync();

        /// <summary>
        /// 获取角色权限ID
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        Task<List<int>> GetPermissionAsync(int id);

        /// <summary>
        /// 分配角色权限
        /// </summary>
        /// <param name="input"></param>
        Task AssignPermissionAsync(AssignPermissionDto input);
    }
}