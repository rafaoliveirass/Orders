using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Orders.Infra.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            var connectionString = "Host=localhost;Database=orders;Username=postgres;Password=admin";

            optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("Orders.Infra"));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
