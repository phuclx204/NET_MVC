using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBusiness.base_class;

namespace BaseBusiness.Model
{
    [Table("payments")]
    public class Payment
    {
        public long Id { get; set; }
        public string TransactionId { get; set; }
        public long OrderId { get; set; }
        public string Method { get; set; }
        public decimal Amount { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public OrderModel Order { get; set; }
    }
}