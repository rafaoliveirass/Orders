using Microsoft.AspNetCore.JsonPatch;
using Orders.Domain.Entities;

namespace Orders.Application.Services.Interfaces
{
     public interface IOrderService
     {
        Task<(List<Order> Items, int TotalCount)> GetAll(Guid? customerId, OrderStatus? status, DateTime? from, DateTime? to, decimal? minTotal, decimal? maxTotal, int page, int pageSize);
        Task<Order> Get(Guid id);
        Task AddAsync(Order order);
        Task ConfirmAsync(Guid id);
        Task CancelAsync(Guid id);
        Task PutAsync(Guid id, Order orderUpdate);
        Task DeleteAsync(Guid id);
    }    
}