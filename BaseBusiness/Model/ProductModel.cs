using System;
using BaseBusiness.bc;
namespace BaseBusiness.Model
{
    public class ProductModel : BaseModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
