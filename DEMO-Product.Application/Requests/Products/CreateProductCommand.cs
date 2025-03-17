using MediatR;

namespace DEMO_Product.Application.Requests.Products
{
    public class CreateProductCommand : IRequest<long>
    {
        public string Name { get; set; }
        public string Description { get; set; } = String.Empty;
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
