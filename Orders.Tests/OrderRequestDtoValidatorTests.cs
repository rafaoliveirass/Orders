using Xunit;
using Orders.Application.Dtos;
using Orders.Application.Validators;

namespace Orders.Tests
{
    public class OrderRequestDtoValidatorTests
    {
        private readonly OrderRequestDtoValidator _validator = new OrderRequestDtoValidator();

        [Fact]
        public void Should_Fail_When_Itens_Is_Null()
        {
            var dto = new OrderRequestDto
            {
                customerId = System.Guid.NewGuid(),
                itens = null!
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Fail_When_Itens_Is_Empty()
        {
            var dto = new OrderRequestDto
            {
                customerId = System.Guid.NewGuid(),
                itens = new List<OrderItemRequestDto>()
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Fail_When_Item_Quantity_Is_Not_Positive()
        {
            var dto = new OrderRequestDto
            {
                customerId = System.Guid.NewGuid(),
                itens = new List<OrderItemRequestDto>
                {
                    new OrderItemRequestDto { ProductId = System.Guid.NewGuid(), Quantity = -10}
                }
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Pass_When_Valid()
        {
            var dto = new OrderRequestDto
            {
                customerId = System.Guid.NewGuid(),
                itens = new List<OrderItemRequestDto>
                {
                    new OrderItemRequestDto { ProductId = System.Guid.NewGuid(), Quantity = 2}
                }
            };

            var result = _validator.Validate(dto);

            Assert.True(result.IsValid);
        }
    }
}
