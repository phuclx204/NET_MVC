using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("products")]
    public class ProductModel : FullAuditModel
    {
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public decimal Weight { get; set; }
        public string Material { get; set; }
        public string Origin { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public string Description { get; set; }

        public CategoryModel Category { get; set; }
        public BrandModel Brand { get; set; }
        public ICollection<ProductDetailModel> ProductDetails { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
