using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("product_details")]
    public class ProductDetailModel : BaseModel
    {
        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ProductModel Product { get; set; }

        public long SizeId { get; set; }
        [ForeignKey("SizeId")]
        public SizeModel Size { get; set; }

        public long ColorId { get; set; }
        [ForeignKey("ColorId")]
        public ColorModel Color { get; set; }

        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public int Stock { get; set; }
        public string SKU { get; set; }
    }
}
