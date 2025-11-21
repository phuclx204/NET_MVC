using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseBusiness.Model
{
    [Table("vouchers")]
    public class VoucherModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("code")]
        [StringLength(50)]
        public string Code { get; set; }

        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; }

        [Column("discount_type")]
        [StringLength(10)]
        public string DiscountType { get; set; } // "tongtien" hoặc "phantram"

        [Column("discount_value")]
        public decimal? DiscountValue { get; set; }

        [Column("min_order")]
        public decimal? MinOrder { get; set; }

        [Column("start_date")]
        public DateTime? StartDate { get; set; }

        [Column("end_date")]
        public DateTime? EndDate { get; set; }

        [Column("status")]
        public byte? Status { get; set; }
    }
}