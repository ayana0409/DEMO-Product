using AutoMapper;
using DEMO_Product.Application.Interfaces.Repositories;
using DEMO_Product.Application.Requests.Products;
using DEMO_Product.Domain.Exception;
using DEMO_Product.Shared.DTO;
using MediatR;

namespace DEMO_Product.Application.UseCases.Product
{
    internal class GetProductByIdHander : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductByIdHander(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetSigleAsync(p => p.IsActive && p.Id.Equals(request.Id))
                ?? throw new NotFoundException();

            var result = _mapper.Map<ProductDto>(product);
            return result;
        }
    }
}
