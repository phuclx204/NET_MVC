using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("categories")]
    public class CategoryModel : BaseModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public int SortOrder { get; set; }
        public long? ParentId { get; set; }

        public CategoryModel Parent { get; set; }
        public ICollection<CategoryModel> Children { get; set; }
        public ICollection<ProductModel> Products { get; set; }
    }
}
