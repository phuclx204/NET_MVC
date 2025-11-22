using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("users")]
    public class UserModel : BaseModel
    {
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }

        [Column("phone")]
        [StringLength(15)]
        public string Phone { get; set; }

        [Column("password")]
        [StringLength(255)]
        public string Password { get; set; }

    }
}