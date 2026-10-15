namespace Orders.Application.Dtos
{
    public class OrderRequestDto
    {
        public Guid customerId { get; set; }
        public string currency { get; set; }
        public required List<OrderItemRequestDto> itens { get; set; }
    }
}
