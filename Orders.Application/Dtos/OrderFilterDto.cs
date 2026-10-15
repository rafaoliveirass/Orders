using Orders.Domain.Entities;

namespace Orders.Application.Dtos
{
    public class OrderFilterDto
    {
        public Guid? CustomerId { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public decimal? MinTotal { get; set; }
        public decimal? MaxTotal { get; set; }
    }
}
