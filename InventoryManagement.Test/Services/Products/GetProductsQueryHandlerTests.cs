using AutoMapper;
using FluentAssertions;
using InventoryManagement.Repository.Model;
using InventoryManagement.Repository.Products;
using InventoryManagement.Service.Products;
using InventoryManagement.Service.Products.Dto;
using InventoryManagement.Service.Products.Mapping;
using InventoryManagement.Test.Builders;
using Moq;


namespace InventoryManagement.Test.Services.Products;

public class GetProductsQueryHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly IMapper _mapper;
    private readonly GetProductsQueryHandler _handler;

    public GetProductsQueryHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ProductMapping());
        });
        _mapper = mapperConfiguration.CreateMapper();

        _handler = new GetProductsQueryHandler(_productRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllProducts()
    {
        // Arrange
        var product = ProductBuilder.Builder()
            .WithName("Test Product")
            .Build();

        var expectedProducts = new List<Product> { product };

        _productRepositoryMock
            .Setup(repository => repository.GetAsync())
            .ReturnsAsync(expectedProducts);

        // Act
        var result = await _handler.Handle(new GetProductsQuery(), CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(_mapper.Map<IEnumerable<ProductResponseDto>>(expectedProducts));
    }
}
