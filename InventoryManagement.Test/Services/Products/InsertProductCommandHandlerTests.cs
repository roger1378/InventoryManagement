using AutoMapper;
using FluentValidation;
using InventoryManagement.Repository.Model;
using InventoryManagement.Repository.Products;
using InventoryManagement.Service.Products;
using InventoryManagement.Service.Products.Dto;
using InventoryManagement.Service.Products.Mapping;
using InventoryManagement.Test.Builders;
using Moq;

namespace InventoryManagement.Test.Services.Products;

public class InsertProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IValidator<ProductCreateRequestDto>> _validatorMock;
    private readonly IMapper _mapper;
    private readonly InsertProductCommandHandler _handler;

    public InsertProductCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _validatorMock = new Mock<IValidator<ProductCreateRequestDto>>();
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ProductMapping());
        });
        _mapper = mapperConfiguration.CreateMapper();

        _handler = new InsertProductCommandHandler(_productRepositoryMock.Object, _mapper, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldInsertProduct()
    {
        // Arrange
        var productCreateRequestDto = new ProductCreateRequestDto()
        {
            Name = "Test Product",
            Price = 10.0m,
            Stock = 10
        };

        var product = ProductBuilder.Builder()
            .WithName("Test Product")
            .Build();

        _productRepositoryMock
            .Setup(repository => repository.InsertAsync(It.IsAny<Product>()))
            .ReturnsAsync(product);

        _productRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Product>())).ReturnsAsync(product);

        // Act
        var result = await _handler.Handle(new InsertProductCommand(productCreateRequestDto), CancellationToken.None);

        // Assert
        _productRepositoryMock.Verify(repo => repo.InsertAsync(It.IsAny<Product>()), Times.Once);
        Assert.Equal(productCreateRequestDto.Name, result.Name);
    }
}
