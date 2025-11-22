using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("stock_in_detail")]
    public class StockInDetailModel : BaseModel
    {
        public long StockInId { get; set; }
        [ForeignKey("StockInId")]
        public StockInModel StockIn { get; set; }

        public long ProductDetailId { get; set; }
        [ForeignKey("ProductDetailId")]
        public ProductDetailModel ProductDetail { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
