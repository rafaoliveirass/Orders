using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Domain.Entities;

namespace Orders.Infra.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {   
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");

            builder.HasKey(o => o.ProductId);
            builder.Property(o => o.ProductId).HasColumnName("productid");
            
            builder.Property(o => o.UnitPrice).HasColumnName("unitprice");
            builder.Property(o => o.Name).HasColumnName("name");
            builder.Property(o => o.AvailableQuantity).HasColumnName("availablequantity");

        }
    }
}
