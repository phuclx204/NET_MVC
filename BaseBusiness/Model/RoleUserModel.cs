using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseBusiness.Model
{
    [Table("role_user")]
    public class RoleUserModel
    {
        // Lưu ý: Trong EF Core cần cấu hình Composite Key (User_Id, Role_Id) trong DbContext
        [Column("user_id")]
        public long UserId { get; set; }

        [Column("role_id")]
        public long RoleId { get; set; }

        // Navigation properties (Optional)
        // [ForeignKey("UserId")]
        // public virtual UserModel User { get; set; }
        // [ForeignKey("RoleId")]
        // public virtual RoleModel Role { get; set; }
    }
}