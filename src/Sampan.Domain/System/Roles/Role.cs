using System.Collections.Generic;
using FreeSql.DataAnnotations;

namespace Sampan.Domain.System
{
    /// <summary>
    /// 角色实体
    /// </summary>
    [Table(Name = "SystemRole")]
    public class Role : SortBaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        [Navigate(ManyToMany = typeof(UserRole))]
        public virtual ICollection<AdminUser> Users { get; set; }

        [Navigate(ManyToMany = typeof(RolePermission))]
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}