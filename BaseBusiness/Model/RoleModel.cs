using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseBusiness.Model
{
    [Table("roles")]
    public class RoleModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; }
    }
}