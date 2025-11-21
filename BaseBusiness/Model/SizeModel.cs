using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("sizes")]
    public class SizeModel : BaseModel
    {
        [Key]
        public long Id { get; set; }

        [Required, MaxLength(20)]
        [Column("name")]
        public string Name { get; set; }

        [Column("status")]
        public byte Status { get; set; } = 1;
    }
}
