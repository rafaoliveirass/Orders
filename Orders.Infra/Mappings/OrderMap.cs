using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Domain.Entities;

namespace Orders.Infra.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("order");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("id");

            builder.Property(o => o.CustomerId).HasColumnName("customerid");
            builder.Property(o => o.Status).HasColumnName("status");
            builder.Property(o => o.Currency).HasColumnName("currency");
            builder.Property(o => o.Total).HasColumnName("total");
            builder.Property(o => o.CreatedAt).HasColumnName("created_at");

        }
    }
}
