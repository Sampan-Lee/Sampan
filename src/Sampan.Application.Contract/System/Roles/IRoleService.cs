using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sampan.Service.Contract.System.Roles
{
    /// <summary>
    /// 权限服务接口
    /// </summary>
    public interface IRoleService : ICrudService<RoleDto, RoleListDto, GetRoleListDto, CreateRoleDto, UpdateRoleDto>
    {
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        Task<List<RolePermissionDto>> GetPermissionAsync(int id);

        /// <summary>
        /// 分配角色权限
        /// </summary>
        /// <param name="input"></param>
        Task AssignPermissionAsync(AssignPermissionDto input);
    }
}