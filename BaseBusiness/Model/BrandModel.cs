using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("brands")]
    public class BrandModel : BaseModel
    {
        [Column("name")]
        public string Name { get; set; }
    }
}
