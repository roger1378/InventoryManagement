using InventoryManagement.Repository.Model;

namespace InventoryManagement.Test.Builders;

public class ProductBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _name = "Test";
    private decimal _price = 10.0m;
    private int _stock = 10;
    
    private ProductBuilder()
    {

    }

    public static ProductBuilder Builder()
    {
        return new ProductBuilder();
    }

    public Product Build()
    {
        return new Product
        {
            Id = _id,
            Name = _name,
            Price = _price,
            Stock = _stock
        };
    }

    internal ProductBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    internal ProductBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    internal ProductBuilder WithPrice(decimal price)
    {
        _price = price;
        return this;
    }

    internal ProductBuilder WithStock(int stock)
    {
        _stock = stock;
        return this;
    }
}
