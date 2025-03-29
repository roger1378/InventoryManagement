using InventoryManagement.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OInventoryManagement.Test.Helpers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RemoveServices(this IServiceCollection services, params Type[] serviceTypes)
    {
        foreach (var descriptor in services
            .Where(descriptor => serviceTypes
                .Any(serviceType => descriptor.ServiceType == serviceType))
            .ToList())
        {
            services.Remove(descriptor);
        }

        return services;
    }

    public static IServiceCollection AddTestDbContextServices(this IServiceCollection services)
    {
        var testId = Guid.NewGuid();
        return services
            .AddDbContext<InventoryManagementDbContext>(
                options => options.UseInMemoryDatabase($"InventoryManagementDb-{testId}", configuration => configuration.UseHierarchyId()));
    }
}
