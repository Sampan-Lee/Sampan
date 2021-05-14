using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.Roles
{
    /// <summary>
    /// 角色业务实体
    /// </summary>
    public class RoleDto : BaseDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}