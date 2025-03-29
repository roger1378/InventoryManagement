using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using InventoryManagement.Repository.Model;
using InventoryManagement.Repository.Products;
using InventoryManagement.Service.Products;
using InventoryManagement.Service.Products.Dto;
using InventoryManagement.Service.Products.Mapping;
using InventoryManagement.Test.Builders;
using Moq;

namespace InventoryManagement.Test.Services.Products;

public class UpdateProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IValidator<ProductUpdateRequestDto>> _validatorMock;
    private readonly IMapper _mapper;
    private readonly UpdateProductCommandHandler _handler;

    public UpdateProductCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _validatorMock = new Mock<IValidator<ProductUpdateRequestDto>>();
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ProductMapping());
        });
        _mapper = mapperConfiguration.CreateMapper();

        _handler = new UpdateProductCommandHandler(_productRepositoryMock.Object, _mapper, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdateProduct()
    {
        // Arrange
        var product = ProductBuilder.Builder()
             .WithName("Test Product")
             .Build();

        _productRepositoryMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        _productRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Product>()))
            .ReturnsAsync(product);

        var productUpdateRequestDto = new ProductUpdateRequestDto()
        {
            Id = product.Id,
            Name = "Test Product",
            Price = 10.0m,
            Stock = 10
        };

        // Act
        var result = await _handler.Handle(new UpdateProductCommand(productUpdateRequestDto), CancellationToken.None);

        // Assert
        _productRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Once);
        Assert.Equal(productUpdateRequestDto.Name, result.Name);
    }
}
