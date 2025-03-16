using DEMO_Product.Domain.Entities;
using DEMO_Product.Shared.DTO;

namespace DEMO_Product.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProductById(long id);
        Task<long> AddProduct(CreateOrUpdateProductDto model);
        Task<ProductDto> UpdateProduct(long id, CreateOrUpdateProductDto model);
        Task DeleteProduct(long id);
    }
}
