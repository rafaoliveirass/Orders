using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Dtos;
using Orders.Application.Erros;
using Orders.Domain.Entities;
using Orders.Infra.Services;

namespace Orders.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly OrderService _orderService;
        private readonly ProductService _productService;
        private readonly IValidator<OrderRequestDto> _orderRequestDtoValidator;

        public OrdersController(IValidator<OrderRequestDto> orderRequestDtoValidator, OrderService orderServce, ProductService productService, TokenService tokenService, ILogger<OrdersController> logger)
        {
            _logger = logger;
            _orderService = orderServce;
            _productService = productService;
            _orderRequestDtoValidator = orderRequestDtoValidator;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAsync([
            FromQuery] Guid? customerId,
            [FromQuery] OrderStatus? status,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] decimal? minTotal,
            [FromQuery] decimal? maxTotal,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var (items, totalCount) = await _orderService.GetAll(customerId, status, from, to, minTotal, maxTotal, page, pageSize);
            Response.Headers["X-Total-Count"] = totalCount.ToString();

            var result = items.Select(item => new OrderResponseDto
            {
                id = item.Id,
                customerId = item.CustomerId,
                currency = item.Currency,
                total = item.Total,
                status = item.Status,
                itens = item.Itens.Select(i => new OrderItemResponsetDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    name = _productService.GetAsync(i.ProductId).Result?.Name ?? "Produto não encontrado",
                }).ToList()
            });

            return Ok(result);
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponseDto>> GetAsync(Guid id)
        {
            var order = await _orderService.Get(id);

            if (order != null)
            {
                return Ok(new OrderResponseDto
                {
                    id = order.Id,
                    total = order.Total,
                    customerId = order.CustomerId,
                    status = order.Status,
                    currency = order.Currency,
                    itens = order.Itens.Select(i => new OrderItemResponsetDto
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        name = _productService.GetAsync(i.ProductId).Result?.Name ?? "Produto não encontrado",
                    }).ToList()
                });
            }

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAsync(OrderRequestDto orderRequest)
        {
            var validationResult = await _orderRequestDtoValidator.ValidateAsync(orderRequest);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.ToCustomValidationFailure());

            try
            {
                Order order = new Order();
                order.Id = new Guid();
                order.Currency = orderRequest.currency;
                order.CustomerId = orderRequest.customerId;
                order.Status = OrderStatus.Placed;

                if (orderRequest.itens != null && orderRequest.itens.Count > 0)
                {
                    foreach (var item in orderRequest.itens)
                    {
                        Product product = await _productService.GetAsync(item.ProductId);

                        if (product == null)
                            return BadRequest($"Produto com ID {item.ProductId} não encontrado.");
                     
                        if (product.AvailableQuantity < item.Quantity)
                            return BadRequest($"Quantidade disponível do produto com ID {item.ProductId} é insuficiente.");
                                                
                        order.Total += product.UnitPrice * item.Quantity;
                        
                        order.Itens.Add(new OrderItem
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            UnitPrice = product.UnitPrice
                        });
                    }
                }

                if (order == null) return BadRequest();

                await _orderService.AddAsync(order);

                await _orderService.SaveChangesAsync();

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar adicionar o pedido");

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno ao processar sua solicitação.");
            }
        }

        [Authorize]
        [HttpPost("{id}/confirm")]
        public async Task<IActionResult> ConfirmAsync(Guid id)
        {
            await _orderService.ConfirmAsync(id);
            await _orderService.SaveChangesAsync();
            return Ok();
        }

        [Authorize]
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelAsync(Guid id)
        {
            await _orderService.CancelAsync(id);
            await _orderService.SaveChangesAsync();
            return Ok();
        }


        // métodos não solicitado pelo teste.

        //[Authorize]
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOrder(Guid id, Order orderUpdate)
        //{
        //    await _orderService.PutAsync(id, orderUpdate);

        //    await _orderService.SaveChangesAsync();

        //    return NoContent();
        //}

        // métodos não solicitado pelo teste.

        //[Authorize]
        //[HttpPatch("{id}")]
        //public async Task<IActionResult> PatchAsync(Guid id, [FromBody] JsonPatchDocument<Order> patchDoc)
        //{
        //    if (patchDoc == null)
        //        return BadRequest();

        //    await _orderService.PatchAsync(id, patchDoc, ModelState);

        //    try
        //    {
        //        await _orderService.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        _logger.LogWarning("Erro ao atualizar os dados.");
        //        throw;
        //    }

        //    return Ok();

        //}

        // métodos não solicitado pelo teste.

        //[Authorize]
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync(Guid id)
        //{
        //    if (id == Guid.Empty)
        //        return BadRequest("O ID do pedido fornecido é inválido.");
        //    try
        //    {
        //        var productExists = await _orderService.Get(id);

        //        if (productExists==null)
        //            return NotFound($"pedido com o ID {id} não foi encontrado.");

        //        await _orderService.DeleteAsync(id);
        //        await _orderService.SaveChangesAsync();

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro ao tentar excluir o pedido com o ID: {PedidoId}", id);

        //        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno ao processar a exclusão.");
        //    }

        //}
    }
}