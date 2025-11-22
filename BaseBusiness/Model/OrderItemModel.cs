using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("order_items")]
    public class OrderItemModel : BaseModel
    {
        [Column("order_id")]
        public long? OrderId { get; set; }

        [Column("product_detail_id")]
        public long? ProductDetailId { get; set; }

        [Column("quantity")]
        public int? Quantity { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("total")]
        public decimal? Total { get; set; }

        // Navigation
        [ForeignKey("OrderId")]
        public virtual OrderModel Order { get; set; }

        [ForeignKey("ProductDetailId")]
        public virtual ProductDetailModel ProductDetail { get; set; }
    }
}