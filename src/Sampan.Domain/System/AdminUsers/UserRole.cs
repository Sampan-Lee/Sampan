using FreeSql.DataAnnotations;

namespace Sampan.Domain.System
{
    [Table(Name = "SystemUserRole")]
    public class UserRole : Entity
    {
        public int UserId { get; set; }

        public virtual AdminUser User { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}