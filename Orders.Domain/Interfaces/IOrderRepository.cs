using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Orders.Domain.Entities;

namespace Orders.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<(List<Order> Items, int TotalCount)> GetAll(Guid? customerId, OrderStatus? status, DateTime? from, DateTime? to, decimal? minTotal, decimal? maxTotal, int page, int pageSize);
        Task<Order> Get(Guid id);
        Task AddAsync(Order order);
        Task ConfirmAsync(Guid id); 
        Task CancelAsync(Guid id);
        Task PutAsync(Guid id, Order orderUpdate);
        Task PatchAsync(Guid id, JsonPatchDocument<Order> patchDoc, ModelStateDictionary modelState);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}