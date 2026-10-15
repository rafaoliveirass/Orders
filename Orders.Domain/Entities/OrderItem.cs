using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.Domain.Entities
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }

        [NotMapped]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}
