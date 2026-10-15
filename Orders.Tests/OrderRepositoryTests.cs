using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;
using Orders.Infra.Context;
using Orders.Infra.Repositories;
using Xunit;

namespace Orders.Tests
{
    public class OrderRepositoryTests
    {
        [Fact]
        public async Task Should_Throw_When_Confirming_Order_With_Insufficient_Stock()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var productId = Guid.NewGuid();

            using (var context = new AppDbContext(options))
            {
                // seed product with available quantity = 1
                context.Products.Add(new Product
                {
                    ProductId = productId,
                    Name = "Test Product",
                    UnitPrice = 10m,
                    AvailableQuantity = 1
                });

                // create order with item quantity = 5
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Guid.NewGuid(),
                    Currency = "BRL",
                    Status = OrderStatus.Placed
                };

                order.Itens.Add(new OrderItem
                {
                    ProductId = productId,
                    Quantity = 5,
                    UnitPrice = 10m
                });

                context.Orders.Add(order);

                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new OrderRepository(context);

                await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                {
                    await repository.ConfirmAsync((await context.Orders.FirstAsync()).Id);
                });
            }
        }
    }
}
