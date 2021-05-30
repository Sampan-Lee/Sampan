using System.ComponentModel.DataAnnotations;
using Sampan.Domain.Shared.System.Roles;
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
        [Required(ErrorMessage = "角色名不能为空")]
        [MaxLength(RoleConst.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Required]
        [MaxLength(RoleConst.MaxDescriptionLength)]
        public string Description { get; set; }
    }
}