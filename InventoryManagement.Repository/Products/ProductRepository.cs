using InventoryManagement.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Repository.Products;

public class ProductRepository : IProductRepository
{
    private readonly InventoryManagementDbContext _inventoryManagementDbContext;

    public ProductRepository(InventoryManagementDbContext inventoryManagementDbContext)
    {
        _inventoryManagementDbContext = inventoryManagementDbContext;
    }

    public async Task<IEnumerable<Product>> GetAsync()
    {
        return await _inventoryManagementDbContext.Products
            .ToListAsync();
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _inventoryManagementDbContext.Products
            .Where(product => product.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<Product> InsertAsync(Product product)
    {
        _inventoryManagementDbContext.Products.Add(product);
        await _inventoryManagementDbContext.SaveChangesAsync();

        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _inventoryManagementDbContext.Update(product).State = EntityState.Modified;
        await _inventoryManagementDbContext.SaveChangesAsync();

        return product;
    }

    public async Task<int> DeleteAsync(Product product)
    {
        _inventoryManagementDbContext.Entry(product).State = EntityState.Deleted;
        return await _inventoryManagementDbContext.SaveChangesAsync();
    }
}
