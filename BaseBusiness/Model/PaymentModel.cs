using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("payments")]
    public class PaymentModel : BaseModel
    {
        [Column("order_id")]
        public long? OrderId { get; set; }

        [Column("method")]
        [StringLength(50)]
        public string Method { get; set; }

        [Column("amount")]
        public decimal? Amount { get; set; }

        [ForeignKey("OrderId")]
        public virtual OrderModel Order { get; set; }
    }
}