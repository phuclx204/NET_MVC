using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBusiness.Model
{
    public class CartItemModel
    {
        public long Id { get; set; }
        public long CartId { get; set; }
        public long ProductDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        public CartModel Cart { get; set; }
        public ProductDetailModel ProductDetail { get; set; }
    }
}
