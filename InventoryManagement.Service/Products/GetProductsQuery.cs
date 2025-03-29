using InventoryManagement.Service.Products.Dto;
using MediatR;

namespace InventoryManagement.Service.Products;

public class GetProductsQuery : IRequest<IEnumerable<ProductResponseDto>>
{
}
