using InventoryManagement.Repository;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Test.Helpers;

public static class SeededInventoryManagementDbContext
{
    public static InventoryManagementDbContext
       BuildPaymentOrdersDbContext()
    {
        var dbContext = new InventoryManagementDbContext(
            new DbContextOptionsBuilder<InventoryManagementDbContext>()
                .UseInMemoryDatabase($"InventoryManagementDb-{Guid.NewGuid():N}", configuration => configuration.UseHierarchyId())
                .Options);

        return dbContext;
    }

    public static InventoryManagementDbContext
       BuildPaymentOrdersDbContext(string databaseName)
    {
        var dbContext = new InventoryManagementDbContext(
            new DbContextOptionsBuilder<InventoryManagementDbContext>()
                .UseInMemoryDatabase(databaseName, configuration => configuration.UseHierarchyId())
                .Options);

        return dbContext;
    }

    public static async Task<InventoryManagementDbContext>
        BuildPaymentOrdersDbContextAsync<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class
    {
        var dbContext = BuildPaymentOrdersDbContext();

        await dbContext.Set<TEntity>().AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        return dbContext;
    }

    public static (InventoryManagementDbContext dbContext, string databaseName)
       BuildPaymentOrdersDbContextWithDatabaseName()
    {
        var databaseName = $"InventoryManagementDb-{Guid.NewGuid():N}";
        var dbContext = new InventoryManagementDbContext(
            new DbContextOptionsBuilder<InventoryManagementDbContext>()
                .UseInMemoryDatabase(databaseName, configuration => configuration.UseHierarchyId())
                .Options);

        return (dbContext, databaseName);
    }
}
