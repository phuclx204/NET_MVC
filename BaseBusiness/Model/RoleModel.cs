using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("roles")]
    public class RoleModel : BaseModel
    {
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; }
    }
}