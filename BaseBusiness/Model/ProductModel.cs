using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("products")]
    public class ProductModel : BaseModel
    {
        [Required, MaxLength(150)]
        public string Name { get; set; }

        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryModel Category { get; set; }

        public long BrandId { get; set; }
        [ForeignKey("BrandId")]
        public BrandModel Brand { get; set; }

        public string Description { get; set; }

        public ICollection<ProductDetailModel> ProductDetails { get; set; }
    }
}
