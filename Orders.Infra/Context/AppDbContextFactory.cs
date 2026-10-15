using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Orders.Infra.Context
{
    // Design-time factory to create AppDbContext for EF Core tools
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Ajuste a connection string para o seu ambiente de desenvolvimento local
            var connectionString = "Host=localhost;Database=orders;Username=postgres;Password=admin";

            optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("Orders.Infra"));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
