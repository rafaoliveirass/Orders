using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Orders.Domain.Entities;

namespace Orders.Domain.Services
{
     public interface IProductService
     {
        Task<Product> GetAsync(Guid id);
        Task PutAsync(Guid id, Product productUpdate);
        Task SaveChangesAsync();
    }    
}