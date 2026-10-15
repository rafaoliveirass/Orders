using Orders.Domain.Entities;

namespace Orders.Application.Dtos
{
    public class OrderResponseDto
    {
        public Guid id { get; set; }
        public Guid customerId { get; set; }
        public string currency { get; set; } = string.Empty;
        public OrderStatus status { get; set; }
        public decimal total { get; set; }
        public required List<OrderItemResponsetDto> itens { get; set; }
    }
}
