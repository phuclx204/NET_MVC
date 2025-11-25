using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("customers")]
    public class CustomerModel : BaseModel
    {
        public string Name { get; set; }
        public byte? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string Tier { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
    }
}