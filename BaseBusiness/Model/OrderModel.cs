using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("orders")]
    public class OrderModel : FullAuditModel
    {
        public long CustomerId { get; set; }
        public long UserId { get; set; }
        public long? VoucherId { get; set; }
        public string Code { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public decimal ShippingFee { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }

        public CustomerModel Customer { get; set; }
        public UserModel User { get; set; }
        public VoucherModel Voucher { get; set; }
        public ICollection<OrderItemModel> OrderItems { get; set; }
    }
}