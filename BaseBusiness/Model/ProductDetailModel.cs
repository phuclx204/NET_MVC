using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("product_details")]
    public class ProductDetailModel : BaseModel
    {
        public string Barcode { get; set; }
        public long ProductId { get; set; }
        public long SizeId { get; set; }
        public long ColorId { get; set; }
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public int MinStock { get; set; }
        public int Stock { get; set; }
        public string Sku { get; set; }

        // Navigation Properties
        public ProductModel Product { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
    }
}
