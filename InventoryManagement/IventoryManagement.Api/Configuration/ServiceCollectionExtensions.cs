using FluentValidation;
using InventoryManagement.Repository.Products;
using InventoryManagement.Service.Products;
using InventoryManagement.Service.Products.Validators;
using OInventoryManagement.Service.Products.Validators;

namespace IventoryManagement.Api.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMappers(
        this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddValidatorsFromAssemblyContaining<ProductCreateRequestDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<ProductUpdateRequestDtoValidator>();

        return services;
    }

    public static IServiceCollection AddMyDependencyGroup(
         this IServiceCollection services)
    {
        //Repositories
        services.AddTransient<IProductRepository, ProductRepository>();

        //Services
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(GetProductsQuery).Assembly,
            typeof(GetProductsQueryHandler).Assembly,
            typeof(GetProductByIdQuery).Assembly,
            typeof(GetProductByIdQueryHandler).Assembly,
            typeof(InsertProductCommand).Assembly,
            typeof(InsertProductCommandHandler).Assembly,
            typeof(UpdateProductCommand).Assembly,
            typeof(UpdateProductCommandHandler).Assembly,
            typeof(DeleteProductCommand).Assembly,
            typeof(DeleteProductCommandHandler).Assembly
            ));

        return services;
    }
}
