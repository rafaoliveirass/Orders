namespace Orders.Application.Dtos
{
    public class OrderItemRequestDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
