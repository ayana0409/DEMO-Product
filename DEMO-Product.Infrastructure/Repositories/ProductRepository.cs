using DEMO_Product.Application.Interfaces.Repositories;
using DEMO_Product.Domain.Entities;
using DEMO_Product.Infrastructure.Persistence;

namespace DEMO_Product.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product, ProductContext>, IProductRepository
    {
        public ProductRepository(ProductContext context) : base(context)
        {
        }

    }
}
