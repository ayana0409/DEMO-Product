using System.ComponentModel.DataAnnotations;
using DEMO_Product.Shared.Entities;

namespace DEMO_Product.Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; } = 0;
        public int Stock { get; set; } = 0;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
