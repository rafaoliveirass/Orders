using Orders.Domain.Entities;

using Orders.Domain.Services;
using Orders.Domain.Repositories;
using Orders.Domain.Entities;

namespace Orders.Infra.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _productRepository.GetAsync(id);
        }

        public Task PutAsync(Guid id, Product productUpdate)
        {
            return _productRepository.PutAsync(id, productUpdate);
        }

        public Task SaveChangesAsync()
        {
            return _productRepository.SaveChangesAsync();
        }
    }
}
