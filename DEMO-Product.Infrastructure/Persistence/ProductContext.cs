using System.Reflection;
using DEMO_Product.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DEMO_Product.Infrastructure.Persistence
{
    public class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
