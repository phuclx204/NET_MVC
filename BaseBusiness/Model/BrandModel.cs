using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("brands")]
    public class BrandModel : BaseModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Logo { get; set; }
    }
}
