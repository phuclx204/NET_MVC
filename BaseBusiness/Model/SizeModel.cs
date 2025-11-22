using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("sizes")]
    public class SizeModel : BaseModel
    {
        [Required, MaxLength(20)]
        [Column("name")]
        public string Name { get; set; }
    }
}
