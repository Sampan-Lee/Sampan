using System.Collections.Generic;

namespace Sampan.Service.Contract.System.SystemUsers
{
    /// <summary>
    /// 修改用户业务实体
    /// </summary>
    public class UpdateAdminUserDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 角色ID集合
        /// </summary>
        public List<int> RoleIds { get; set; }
    }
}