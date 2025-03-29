using FluentAssertions;
using InventoryManagement.Service.Products.Dto;
using OInventoryManagement.Service.Products.Validators;

namespace InventoryManagement.Test.Services.Products.Validators;

public class ProductCreateRequestDtoValidatorTest
{
    private readonly ProductCreateRequestDtoValidator _validator;

    public ProductCreateRequestDtoValidatorTest()
    {
        _validator = new ProductCreateRequestDtoValidator();
    }

    [Fact]
    public async Task ValidateAsync_WhenNameIsValid_ShouldReturnIsValid()
    {
        // ARRANGE
        var productCreateRequestDto = new ProductCreateRequestDto
        {
            Name = "My Product",
            Price = 10.0m,
            Stock = 10
        };

        // ACT
        var result = await _validator.ValidateAsync(productCreateRequestDto);

        // ASSERT
        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public async Task ValidateAsync_WhenNameIsNullOrEmpty_ShouldReturnNameIsMissing()
    {
        // ARRANGE
        var productCreateRequestDto = new ProductCreateRequestDto
        {
            Name = string.Empty,
            Price = 10.0m,
            Stock = 10
        };

        // ACT
        var result = await _validator.ValidateAsync(productCreateRequestDto);

        // ASSERT
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.PropertyName == nameof(productCreateRequestDto.Name));
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public async Task ValidateAsync_WhenUnitPriceIsNullOrEmpty_ShouldReturnUnitPriceIsMissing()
    {
        // ARRANGE
        var productCreateRequestDto = new ProductCreateRequestDto
        {
            Name = "My Product",
            Price = 0,
            Stock = 10
        };

        // ACT
        var result = await _validator.ValidateAsync(productCreateRequestDto);

        // ASSERT
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.PropertyName == nameof(productCreateRequestDto.Price));
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public async Task ValidateAsync_WhenStockIsNullOrEmpty_ShouldReturnStockIsMissing()
    {
        // ARRANGE
        var productCreateRequestDto = new ProductCreateRequestDto
        {
            Name = "My Product",
            Price = 10.0M,
            Stock = 0
        };

        // ACT
        var result = await _validator.ValidateAsync(productCreateRequestDto);

        // ASSERT
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.PropertyName == nameof(productCreateRequestDto.Stock));
        result.Errors.Should().HaveCount(1);
    }
}
