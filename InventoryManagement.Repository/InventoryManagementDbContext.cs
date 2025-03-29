using InventoryManagement.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Repository;

public class InventoryManagementDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InventoryManagementDbContext"/> class.
    /// <see cref="InventoryManagementDbContext"/> Constructor.
    /// </summary>
    /// <param name="options">Dependency injection options to set up the possibles configurations.</param>
    public InventoryManagementDbContext(DbContextOptions<InventoryManagementDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}
