using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseBusiness.Model
{
    [Table("suppliers")]
    public class SupplierModel
    {
        [Key]
        public long Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public string Address { get; set; }
    }
}
