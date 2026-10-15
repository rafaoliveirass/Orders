using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;
using Orders.Infra.Mappings;

namespace Orders.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new OrderItemMap());
            
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = Guid.Parse("d4f7fecb-aa31-4789-8f14-ee6f72f8b539"), Name = "Coca-cola", UnitPrice = 10, AvailableQuantity = 10},
                new Product { ProductId = Guid.Parse("f4b3e8c1-9a72-41e5-82b3-6c19f0a2d48f"), Name = "Pepsi", UnitPrice = 9.50M, AvailableQuantity = 10}
             );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = Guid.Parse("557bc0cb-a88d-44cf-a072-1efdaed024c9"), Name = "Client1"}
             );

            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new ProductMap());

        }
    }

}

