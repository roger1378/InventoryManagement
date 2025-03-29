using AutoMapper;
using FluentAssertions;
using InventoryManagement.Repository.Products;
using InventoryManagement.Service.Products;
using InventoryManagement.Service.Products.Dto;
using InventoryManagement.Service.Products.Mapping;
using InventoryManagement.Test.Builders;
using Moq;

namespace InventoryManagement.Test.Services.Products;

public class GetProductByIdQueryHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly IMapper _mapper;
    private readonly GetProductByIdQueryHandler _handler;

    public GetProductByIdQueryHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ProductMapping());
        });
        _mapper = mapperConfiguration.CreateMapper();

        _handler = new GetProductByIdQueryHandler(_productRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnProductById()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = ProductBuilder.Builder()
            .WithId(productId)
            .WithName("Test Product")
            .Build();

        _productRepositoryMock
            .Setup(repository => repository.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        // Act
        var result = await _handler.Handle(new GetProductByIdQuery(productId), CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(_mapper.Map<ProductResponseDto>(product));
    }
}
