using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Domain.Entities;

namespace Orders.Infra.Mappings
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_item");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).HasColumnName("idorderitem");

            builder.Property(i => i.OrderId).HasColumnName("orderid");
            builder.Property(i => i.ProductId).HasColumnName("productid");
            builder.Property(i => i.Quantity).HasColumnName("quantity");

            builder.HasOne<Order>()
                .WithMany(o => o.Itens)
                .HasForeignKey(i => i.OrderId)
                .HasPrincipalKey(o => o.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
