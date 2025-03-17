using AutoMapper;
using DEMO_Product.Application.Interfaces.Repositories;
using DEMO_Product.Application.Requests.Products;
using DEMO_Product.Shared.DTO;
using MediatR;

namespace DEMO_Product.Application.UseCases.Product
{
    internal class GetAllProductHandler : IRequestHandler<GetAllProductQuery, List<ProductDto>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetAllProductHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync(p => p.IsActive);
            var result = _mapper.Map<List<ProductDto>>(products);

            return result;
        }
    }
}
