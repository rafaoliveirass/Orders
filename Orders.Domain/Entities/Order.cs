using System.ComponentModel.DataAnnotations;

namespace Orders.Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public string Currency { get; set; }
        public List<OrderItem> Itens { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
	
 	public Order()
        {
            Id = Guid.NewGuid();
            Status = OrderStatus.Draft;
            Itens = new List<OrderItem>();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
