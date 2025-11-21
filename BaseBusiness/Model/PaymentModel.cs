using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseBusiness.Model
{
    [Table("payments")]
    public class PaymentModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("order_id")]
        public long? OrderId { get; set; }

        [Column("method")]
        [StringLength(50)]
        public string Method { get; set; } // cash, card, momo...

        [Column("amount")]
        public decimal? Amount { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("OrderId")]
        public virtual OrderModel Order { get; set; }
    }
}