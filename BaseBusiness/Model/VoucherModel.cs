using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("vouchers")]
    public class VoucherModel
    {
        [Key]
        public long Id { get; set; }
        public string Code { get; set; }
        public long? UserId { get; set; }
        public int UsageLimit { get; set; }
        public int UsedCount { get; set; }
        public string Description { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal MaxDiscount { get; set; }
        public decimal MinOrder { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte Status { get; set; }

        public UserModel User { get; set; }
    }
}