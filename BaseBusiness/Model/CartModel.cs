using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    public class CartModel : BaseModel
    {
        public long CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public int ItemCount { get; set; }
        public string CartStatus { get; set; } // PENDING/CHECKOUT/ABANDONED

        public CustomerModel Customer { get; set; }
        public ICollection<CartItemModel> CartItems { get; set; }
    }
}
