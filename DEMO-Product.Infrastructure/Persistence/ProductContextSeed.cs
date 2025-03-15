using DEMO_Product.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DEMO_Product.Infrastructure.Persistence
{
    public class ProductContextSeed
    {
        private readonly ILogger<ProductContextSeed> _logger;
        private readonly ProductContext _context;

        public ProductContextSeed(ILogger<ProductContextSeed> logger, ProductContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_context.Database.IsMySql())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!_context.Products.Any())
            {
                await _context.Products.AddRangeAsync(
                    new Product
                    {
                        Name = "Product 1",
                        Description = "Description 1",
                        Price = 1500
                    },
                    new Product
                    {
                        Name = "Product 2",
                        Description = "Description 2",
                        Price = 1700
                    },
                    new Product
                    {
                        Name = "Product 3",
                        Description = "Description 3",
                        Price = 1100
                    }
                );
                await _context.SaveChangesAsync();
            }
        }
    }
}
