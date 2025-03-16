using AutoMapper;
using DEMO_Product.Application.Interfaces.Repositories;
using DEMO_Product.Application.Interfaces.Services;
using DEMO_Product.Domain.Entities;
using DEMO_Product.Domain.Exception;
using DEMO_Product.Shared.DTO;

namespace DEMO_Product.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<ProductDto>> GetAllProducts()
        {
            var products = await _repository
                .GetAllAsync(p => p.IsActive);
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductById(long id)
        {
            var products = await _repository
                .GetSigleAsync(p => p.Id.Equals(id) && p.IsActive);
            return _mapper.Map<ProductDto>(products);
        }

        public async Task<long> AddProduct(CreateOrUpdateProductDto model)
        {
            var product = _mapper.Map<Product>(model);
            await _repository.CreateAsync(product);
            return product.Id;
        }
        public async Task<ProductDto> UpdateProduct(long id, CreateOrUpdateProductDto model)
        {
            var existProduct = await _repository.GetSigleAsync(p => p.Id.Equals(id))
                ?? throw new NotFoundException();

            var product = _mapper.Map(model, existProduct);
            _repository.Update(product);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task DeleteProduct(long id)
        {
            var existProduct = await _repository.GetSigleAsync(p => p.Id.Equals(id))
                ?? throw new NotFoundException();

            existProduct.IsActive = false;
            _repository.Update(existProduct);
        }

    }
}
