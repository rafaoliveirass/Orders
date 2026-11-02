using Orders.Application.Services.Interfaces;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;

namespace Orders.Application.Services
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<(List<Order> Items, int TotalCount)> GetAll(Guid? customerId, OrderStatus? status, DateTime? from, DateTime? to, decimal? minTotal, decimal? maxTotal, int page, int pageSize)
        {
            return await _orderRepository.GetAll(customerId, status, from, to, minTotal, maxTotal, page, pageSize);
        }

        public async Task<Order> Get(Guid id)
        {
            return await _orderRepository.Get(id);
        }

        public async Task AddAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();
        }
        public async Task ConfirmAsync(Guid id)
        {
            await _orderRepository.ConfirmAsync(id);
            await _orderRepository.SaveChangesAsync();
        }
        public async Task CancelAsync(Guid id)
        {
            await _orderRepository.CancelAsync(id);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task PutAsync(Guid id, Order orderUpdate)
        {
            await _orderRepository.PutAsync(id, orderUpdate);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _orderRepository.DeleteAsync(id);
            await _orderRepository.SaveChangesAsync();
        }

    }
}
