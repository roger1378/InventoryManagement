using FluentAssertions;
using InventoryManagement.Repository.Model;
using InventoryManagement.Repository.Products;
using InventoryManagement.Test.Builders;
using InventoryManagement.Test.Helpers;

namespace InventoryManagement.Test.Repositories.Products;

public class ProductRepositoryTests
{
    [Fact]
    public async Task GetAsync_WhenRequested_ShouldReturnAllActiveProducts()
    {
        var product = ProductBuilder.Builder()
            .Build();

        var dbContext = await SeededInventoryManagementDbContext
            .BuildPaymentOrdersDbContextAsync(new Product[] { product });

        var repository = new ProductRepository(dbContext);

        // ACT
        var actualProduct = await repository.GetAsync();
        var expectedProduct = new Product[] { product };

        // ASSERT
        expectedProduct.Should().Equal(actualProduct);
    }

    [Fact]
    public async Task GetByIdAsync_WhenRequested_ShouldReturnProductById()
    {
        var product = ProductBuilder.Builder()
            .Build();

        var dbContext = await SeededInventoryManagementDbContext
            .BuildPaymentOrdersDbContextAsync(new Product[] { product });

        var repository = new ProductRepository(dbContext);

        // ACT
        var actualProduct = await repository.GetByIdAsync(product.Id);

        // ASSERT
        actualProduct.Should().BeEquivalentTo(actualProduct);
    }

    [Fact]
    public async Task InsertAsync_WhenRequested_ShouldInsertProduct()
    {
        using var dbContext = SeededInventoryManagementDbContext
            .BuildPaymentOrdersDbContext();

        var product = ProductBuilder.Builder()
            .Build();

        var repository = new ProductRepository(dbContext);

        // ACT
        var result = await repository.InsertAsync(product);

        // ASSERT
        dbContext.Set<Product>().Should().Equal(new List<Product> { product });
    }

    [Fact]
    public async Task UpdateAsync_WhenRequested_ShouldUpdateProduct()
    {
        var product = ProductBuilder.Builder()
            .Build();

        var dbContext = await SeededInventoryManagementDbContext
           .BuildPaymentOrdersDbContextAsync(new Product[] { product });

        var repository = new ProductRepository(dbContext);

        // ACT
        await repository.UpdateAsync(product);

        // ASSERT
        dbContext.Set<Product>().Should().Equal(new List<Product> { product });
    }

    [Fact]
    public async Task DeleteAsync_WhenRequested_ShouldDeleteProduct()
    {
        var product = ProductBuilder.Builder()
            .Build();

        var dbContext = await SeededInventoryManagementDbContext
           .BuildPaymentOrdersDbContextAsync(new Product[] { product });

        var repository = new ProductRepository(dbContext);

        // ACT
        var affectedRows = await repository.DeleteAsync(product);

        // ASSERT
        affectedRows.Should().Be(1);
    }
}
