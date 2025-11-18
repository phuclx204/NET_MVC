using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseBusiness.Model
{
    [Table("colors")]
    public class ColorModel
    {
        [Key]
        public long Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        public byte Status { get; set; } = 1;
    }
}
