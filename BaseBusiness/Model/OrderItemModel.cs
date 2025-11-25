using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("order_items")]
    public class OrderItemModel
    {
        [Key]
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long ProductDetailId { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }

        public virtual OrderModel Order { get; set; }
        public virtual ProductDetailModel ProductDetail { get; set; }
    }
}