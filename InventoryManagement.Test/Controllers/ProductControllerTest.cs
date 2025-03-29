using InventoryManagement.Service.Products;
using InventoryManagement.Service.Products.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Orders.Api.Controllers.Products;

namespace InventoryManagement.Test.Controllers;

public class ProductControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _controller = new ProductController(_mockMediator.Object);
    }

    [Fact]
    public async Task GetAsync_ReturnsOkResult_WithListOfProducts()
    {
        // Arrange
        var products = new List<ProductResponseDto>
        {
            new ProductResponseDto { Id = Guid.NewGuid(), Name = "Product1", Price = 10, Stock = 1 },
            new ProductResponseDto { Id = Guid.NewGuid(), Name = "Product2", Price = 20, Stock = 2 }
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetProductsQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(products);

        // Act
        var result = await _controller.GetAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<ProductResponseDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
        Assert.Equal(products, returnValue);
    }

    [Fact]
    public async Task InsertAsync_ReturnsCreatedResult_WithProduct()
    {
        // Arrange
        var productCreateRequest = new ProductCreateRequestDto { Name = "Product1", Price = 10, Stock = 5 };
        var productResponse = new ProductResponseDto { Id = Guid.NewGuid(), Name = "Product1", Price = 10, Stock = 5 };
        _mockMediator.Setup(m => m.Send(It.IsAny<InsertProductCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(productResponse);

        // Act
        var result = await _controller.InsertAsync(productCreateRequest);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        var returnValue = Assert.IsType<ProductResponseDto>(createdResult.Value);
        Assert.Equal(productCreateRequest.Name, returnValue.Name);
        Assert.Equal(productCreateRequest.Price, returnValue.Price);
        Assert.Equal(productCreateRequest.Stock, returnValue.Stock);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsOkResult_WithUpdatedProduct()
    {
        // Arrange
        var productUpdateRequest = new ProductUpdateRequestDto { Id = Guid.NewGuid(), Name = "UpdatedProduct", Price = 15, Stock = 10 };
        var productResponse = new ProductResponseDto { Id = productUpdateRequest.Id, Name = "UpdatedProduct", Price = 15, Stock = 10 };
        _mockMediator.Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(productResponse);

        // Act
        var result = await _controller.UpdateAsync(productUpdateRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ProductResponseDto>(okResult.Value);
        Assert.Equal(productUpdateRequest.Name, returnValue.Name);
        Assert.Equal(productUpdateRequest.Price, returnValue.Price);
        Assert.Equal(productUpdateRequest.Stock, returnValue.Stock);
    }
}
