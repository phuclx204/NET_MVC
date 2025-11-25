using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("suppliers")]
    public class SupplierModel
    {
        [Key]
        public long Id { get; set; }
        public string TaxCode { get; set; }
        public string Name { get; set; }
        public string ContacPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
    }
}
