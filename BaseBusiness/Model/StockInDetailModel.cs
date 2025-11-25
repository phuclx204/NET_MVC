using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("stock_in_detail")]
    public class StockInDetailModel
    {
        [Key]
        public long Id { get; set; }
        public long StockInId { get; set; }
        public long ProductDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public StockInModel StockIn { get; set; }
        public ProductDetailModel ProductDetail { get; set; }


    }
}
