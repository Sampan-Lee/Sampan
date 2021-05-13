using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.Roles
{
    /// <summary>
    /// 角色列表业务实体
    /// </summary>
    public class RoleListDto : BaseDto
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