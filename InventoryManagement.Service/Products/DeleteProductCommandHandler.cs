using InventoryManagement.Repository.Model;
using InventoryManagement.Repository.Products;
using MediatR;

namespace InventoryManagement.Service.Products;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await AssertProductExists(request.Id);

        return await _productRepository.DeleteAsync(product);
    }

    private async Task<Product> AssertProductExists(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"The Product with Id {id} could not be found.");
        }

        return product;
    }
}
