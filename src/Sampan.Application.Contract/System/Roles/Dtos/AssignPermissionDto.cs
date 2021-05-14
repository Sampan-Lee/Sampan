using System.Collections.Generic;

namespace Sampan.Service.Contract.System.Roles
{
    /// <summary>
    /// 分配角色权限业务实体
    /// </summary>
    public class AssignPermissionDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 权限ID集合
        /// </summary>
        public List<int> PermissionIds { get; set; }
    }
}