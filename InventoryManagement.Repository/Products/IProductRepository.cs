using InventoryManagement.Repository.Model;

namespace InventoryManagement.Repository.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAsync();

    Task<Product> GetByIdAsync(Guid id);

    Task<Product> InsertAsync(Product product);

    Task<Product> UpdateAsync(Product product);

    Task<int> DeleteAsync(Product product);
}
