using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseBusiness.Model
{
    [Table("product_details")]
    public class ProductDetailModel
    {
        [Key]
        public long Id { get; set; }

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
        public byte Status { get; set; } = 1;
    }
}
