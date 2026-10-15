using Xunit;
using Orders.Domain.Entities;

namespace Orders.Tests
{
    public class OrderEntityTests
    {
        [Fact]
        public void NewOrder_Should_Have_Defaults()
        {
            var order = new Order();

            Assert.NotEqual(Guid.Empty, order.Id);
            Assert.Equal(OrderStatus.Draft, order.Status);
            Assert.NotNull(order.Itens);
            Assert.Empty(order.Itens);
            Assert.True(order.CreatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public void Total_Should_Be_Summation_Of_Items()
        {
            var order = new Order();

            order.Itens = new List<OrderItem>
            {
                new OrderItem { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 3.5m },
                new OrderItem { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 4m }
            };

            var expectedTotal = 2 * 3.5m + 1 * 4m;

            order.Total = 0m;
            order.Total = order.Itens.Sum(i => i.UnitPrice * i.Quantity);

            Assert.Equal(expectedTotal, order.Total);
        }
    }
}
