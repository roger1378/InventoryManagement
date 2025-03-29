namespace InventoryManagement.Service.Products.Dto;

public class ProductCreateRequestDto
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }
}
