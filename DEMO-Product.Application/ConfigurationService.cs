using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DEMO_Product.Application
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
            return services;
        }
    }
}
