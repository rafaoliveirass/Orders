using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;
using Orders.Domain.Services;

namespace Orders.Infra.Services
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

        public Task AddAsync(Order order)
        {
            return _orderRepository.AddAsync(order);
        }
        public Task ConfirmAsync(Guid id)
        {
            return _orderRepository.ConfirmAsync(id);
        }
        public Task CancelAsync(Guid id)
        {
            return _orderRepository.CancelAsync(id);
        }

        public Task PutAsync(Guid id, Order orderUpdate)
        {
            return _orderRepository.PutAsync(id, orderUpdate);
        }

        public Task PatchAsync(Guid id, JsonPatchDocument<Order> patchDoc, ModelStateDictionary modelState)
        {
            return _orderRepository.PatchAsync(id, patchDoc, modelState);
        }

        public Task DeleteAsync(Guid id)
        {
            return _orderRepository.DeleteAsync(id);
        }

        public Task SaveChangesAsync()
        {
            return _orderRepository.SaveChangesAsync();
        }
    }
}
