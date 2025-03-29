using InventoryManagement.Repository.Model;
using InventoryManagement.Repository.Products;
using InventoryManagement.Service.Products;
using InventoryManagement.Test.Builders;
using Moq;

namespace InventoryManagement.Test.Services.Products;

public class DeleteProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly DeleteProductCommandHandler _handler;

    public DeleteProductCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _handler = new DeleteProductCommandHandler(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteProduct()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = ProductBuilder.Builder()
            .WithId(productId)
            .Build();

        _productRepositoryMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        _productRepositoryMock
            .Setup(r => r.DeleteAsync(It.IsAny<Product>()))
            .ReturnsAsync(1);

        // Act
        await _handler.Handle(new DeleteProductCommand(productId), CancellationToken.None);

        // Assert
        _productRepositoryMock.Verify(repo => repo.DeleteAsync(product), Times.Once);
    }
}
