﻿namespace InventoryManagement.Service.Products.Dto;

public class ProductUpdateRequestDto
{
    public  Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }
}
