using AutoMapper;
using DEMO_Product.Application.Interfaces.Repositories;
using DEMO_Product.Application.Requests.Products;
using MediatR;

namespace DEMO_Product.Application.UseCases.Product
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, long>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public CreateProductHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        { 
            var product = _mapper.Map<Domain.Entities.Product>(request);
            await _repository.CreateAsync(product);
            return product.Id;
        }
    }
}
