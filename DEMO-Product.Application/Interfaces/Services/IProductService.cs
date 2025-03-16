using DEMO_Product.Domain.Entities;
using DEMO_Product.Shared.DTO;

namespace DEMO_Product.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProductById(long id);
        Task<long> AddProduct(CreateProductDto model);
        Task<ProductDto> UpdateProduct(long id, UpdateProductDto model);
        Task DeleteProduct(long id);
    }
}
