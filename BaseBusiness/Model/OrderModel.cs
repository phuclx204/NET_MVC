using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseBusiness.Model
{
    [Table("orders")]
    public class OrderModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("customer_id")]
        public long? CustomerId { get; set; }

        [Column("user_id")]
        public long? UserId { get; set; } // Người tạo đơn (nhân viên)

        [Column("voucher_id")]
        public long? VoucherId { get; set; }

        [Column("total_amount")]
        public decimal? TotalAmount { get; set; }

        [Column("discount_amount")]
        public decimal? DiscountAmount { get; set; }

        [Column("final_amount")]
        public decimal? FinalAmount { get; set; }

        [Column("status")]
        [StringLength(30)]
        public string Status { get; set; } // Pending, Completed, Cancelled...

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        // Foreign Keys
        [ForeignKey("CustomerId")]
        public virtual CustomerModel Customer { get; set; }

        [ForeignKey("VoucherId")]
        public virtual VoucherModel Voucher { get; set; }
    }
}