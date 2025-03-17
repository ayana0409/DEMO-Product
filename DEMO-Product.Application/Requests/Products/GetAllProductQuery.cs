using DEMO_Product.Shared.DTO;
using MediatR;

namespace DEMO_Product.Application.Requests.Products
{
    public class GetAllProductQuery : IRequest<List<ProductDto>>
    {
    }
}
