using FreeSql.DataAnnotations;

namespace Sampan.Domain.System
{
    [Table(Name = "SystemRolePermission")]
    public class RolePermission : TenantEntity
    {
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
    }
}