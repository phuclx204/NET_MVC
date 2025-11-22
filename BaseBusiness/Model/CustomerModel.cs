using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("customers")]
    public class CustomerModel : BaseModel
    {
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Column("phone")]
        [StringLength(15)]
        public string Phone { get; set; }

        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }

        [Column("address")]
        [StringLength(255)]
        public string Address { get; set; }
    }
}