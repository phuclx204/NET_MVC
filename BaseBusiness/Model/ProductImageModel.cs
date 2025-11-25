using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBusiness.Model
{
    [Table("product_images")]
    public class ProductImage
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Url { get; set; }
        public decimal Amount { get; set; }
        public bool IsThumbnail { get; set; }

        public ProductModel Product { get; set; }
    }
}
