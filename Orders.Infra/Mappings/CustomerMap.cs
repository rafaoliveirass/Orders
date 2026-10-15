using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Domain.Entities;

namespace Orders.Infra.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {   
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customer");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("id");
            
            builder.Property(o => o.Name).HasColumnName("name");

        }
    }
}
