using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseBusiness.Model
{
    [Table("sizes")]
    public class SizeModel
    {
        [Key]
        public long Id { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        public byte Status { get; set; } = 1;
    }
}
