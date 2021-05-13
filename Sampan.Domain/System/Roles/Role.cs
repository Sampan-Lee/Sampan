using System.Collections.Generic;
using FreeSql.DataAnnotations;
using Sampan.Public.Entity;

namespace Sampan.Domain.System
{
    /// <summary>
    /// 角色实体
    /// </summary>
    [Table(Name = "SystemRole")]
    public class Role : SortBaseEntity
    {
        public string Name { get; set; }

        [Navigate(ManyToMany = typeof(UserRole))]
        public virtual ICollection<AdminUser> Users { get; set; }

        [Navigate(ManyToMany = typeof(RolePermission))]
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}