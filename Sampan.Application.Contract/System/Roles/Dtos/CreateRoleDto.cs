using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.Roles
{
    /// <summary>
    /// 创建角色业务实体
    /// </summary>
    public class CreateRoleDto : SortDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}