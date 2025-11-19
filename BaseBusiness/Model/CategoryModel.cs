using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("categories")]
    public class CategoryModel : BaseModel
    {
        [Column("name")]
        public string Name { get; set; }

        public long? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public CategoryModel Parent { get; set; }

        public ICollection<CategoryModel> Children { get; set; }
    }
}
