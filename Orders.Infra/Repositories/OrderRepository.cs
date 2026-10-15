using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;
using Orders.Infra.Context;

namespace Orders.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<(List<Order> Items, int TotalCount)> GetAll(Guid? customerId, OrderStatus? status, DateTime? from, DateTime? to, decimal? minTotal, decimal? maxTotal, int page, int pageSize)
        {
            var query = _context.Orders.AsQueryable();

            if (customerId.HasValue)
                query = query.Where(p => p.CustomerId == customerId.Value);

            if (status.HasValue)
                query = query.Where(p => p.Status == status.Value);

            if (from.HasValue)
                query = query.Where(p => p.CreatedAt >= from.Value);

            if (to.HasValue)
                query = query.Where(p => p.CreatedAt <= to.Value);

            if (minTotal.HasValue)
                query = query.Where(p => p.Total >= minTotal.Value);

            if (maxTotal.HasValue)
                query = query.Where(p => p.Total <= maxTotal.Value);

            var total = await query.CountAsync();

            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var items = await query
               .OrderByDescending(p => p.CreatedAt)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .Select(p => new Order
               {
                   Id = p.Id,
                   CustomerId = p.CustomerId,
                   Status = p.Status,
                   Currency = p.Currency,
                   Itens = p.Itens,
                   Total = p.Total,
                   CreatedAt = p.CreatedAt
               })
               .ToListAsync();

            return (items, total);
        }

        async Task<Order> IOrderRepository.Get(Guid id)
        {
            var query = _context.Orders.AsQueryable();
                query = query.Where(p => p.Id == id);

            var item = await query.Where(p => p.Id == id)
               .Select(p => new Order
               {
                   Id = p.Id,
                   CustomerId = p.CustomerId,
                   Status = p.Status,
                   Currency = p.Currency,
                   Itens = p.Itens,
                   Total = p.Total,
                   CreatedAt = p.CreatedAt
               }).FirstOrDefaultAsync();

            return item ?? new Order();
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Orders.FindAsync(id);
            
            if (entity != null)
            {
                _context.Orders.Remove(entity);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task PutAsync(Guid id, Order orderUpdate)
        {
            var entity = await _context.Orders.FindAsync(id);

            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(orderUpdate);
            }
        }

        public async Task PatchAsync(Guid id, JsonPatchDocument<Order> patchDoc, ModelStateDictionary modelState)
        {
            var entity = await _context.Orders.FindAsync(id);

            if (entity != null)
            {
                patchDoc.ApplyTo(entity, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter) modelState);

            }
        }

        public async Task ConfirmAsync(Guid id)
        {
            var entity = await _context.Orders
                .Include(o => o.Itens)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (entity != null)
            {
                if (entity.Status == OrderStatus.Placed)
                {
                    entity.Status = OrderStatus.Confirmed;

                    await UpdateStock(entity, false);
                }
            }
        }

        public async Task CancelAsync(Guid id)
        {
            var entity = await _context.Orders
                .Include(o => o.Itens)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (entity != null)
            {
                if (entity.Status == OrderStatus.Placed
                    || entity.Status == OrderStatus.Confirmed)
                {
                    entity.Status = OrderStatus.Canceled;

                    await UpdateStock(entity, true);
                }
            }
        }

        public async Task UpdateStock(Order order, bool replenish)
        {
            foreach (OrderItem item in order.Itens)
            {
                var entityProduct = await _context.Products.FindAsync(item.ProductId);

                if (entityProduct != null)
                {
                    if (replenish)
                    {
                        entityProduct.AvailableQuantity += item.Quantity;
                    }
                    else
                    {
                        if (entityProduct.AvailableQuantity < item.Quantity)
                            throw new InvalidOperationException($"Estoque insuficiente para o produto {entityProduct.Name}. Disponível no momento : {entityProduct.AvailableQuantity}, Precisa ter : {item.Quantity}");

                        entityProduct.AvailableQuantity -= item.Quantity;
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}