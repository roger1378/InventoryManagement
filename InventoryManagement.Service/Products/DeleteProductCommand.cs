using InventoryManagement.Service.Products.Dto;
using MediatR;

namespace InventoryManagement.Service.Products;

public class DeleteProductCommand : IRequest<int>
{
    public Guid Id { get; set; }

    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
}