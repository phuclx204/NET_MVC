using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("stock_in")]
    public class StockInModel : FullAuditModel
    {
        public string Code { get; set; }
        public long SupplierId { get; set; }
        public long UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Note { get; set; }

        public SupplierModel Supplier { get; set; }
        public UserModel User { get; set; }
        public ICollection<StockInDetailModel> StockInDetails { get; set; }
    }
}
