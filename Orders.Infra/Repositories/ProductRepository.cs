using Microsoft.EntityFrameworkCore;
using Orders.Infra.Context;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;

namespace Orders.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            this._context = context;
        }
        async Task<Product> IProductRepository.GetAsync(Guid id)
        {
            return await _context.Products.Where(x => x.ProductId == id).FirstAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task PutAsync(Guid id, Product productUpdate)
        {
            var entity = await _context.Orders.FindAsync(id);

            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(productUpdate);
            }
        }
    }
}