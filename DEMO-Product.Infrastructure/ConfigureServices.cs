using DEMO_Product.Application.Interfaces.Repositories;
using DEMO_Product.Application.Interfaces.Services;
using DEMO_Product.Application.Mappings;
using DEMO_Product.Application.Services;
using DEMO_Product.Infrastructure.Persistence;
using DEMO_Product.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DEMO_Product.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString")
                                   ?? throw new ArgumentNullException("Connection string is not configured.");

            services.AddDbContext<ProductContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<ProductContextSeed>();
            services.AddAutoMapper(config => config.AddProfile(new MappingProfile()));
            return services;
        }

        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
            => services
                .AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<IProductService, ProductService>();
    }
}
