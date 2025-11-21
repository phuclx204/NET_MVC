using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BaseBusiness.Model
{
    [Table("stock_in")]
    public class StockInModel
    {
        [Key]
        public long Id { get; set; }

        public long SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public SupplierModel Supplier { get; set; }

        public long UserId { get; set; } // Người tạo phiếu
        // Có thể thêm ForeignKey tới User nếu cần

        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<StockInDetailModel> StockInDetails { get; set; }
    }
}
