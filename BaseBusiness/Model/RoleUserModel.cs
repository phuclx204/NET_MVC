using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseBusiness.Model
{
    [Table("role_user")]
    public class RoleUserModel
    {
        [Column("user_id")]
        public long UserId { get; set; }

        [Column("role_id")]
        public long RoleId { get; set; }

    }
}