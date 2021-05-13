using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.Roles
{
    /// <summary>
    /// 获取角色列表业务实体
    /// </summary>
    public class GetRoleListDto : GetPageDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}