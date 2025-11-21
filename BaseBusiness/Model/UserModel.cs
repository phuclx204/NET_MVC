using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseBusiness.Model
{
    [Table("users")]
    public class UserModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

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

        [Column("status")]
        public byte Status { get; set; } = 1;

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}