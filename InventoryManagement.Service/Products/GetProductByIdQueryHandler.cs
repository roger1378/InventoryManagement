using AutoMapper;
using InventoryManagement.Repository.Model;
using InventoryManagement.Repository.Products;
using InventoryManagement.Service.Products.Dto;
using MediatR;

namespace InventoryManagement.Service.Products;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponseDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await AssertProductExists(request.Id);
        
        return _mapper.Map<ProductResponseDto>(product);
    }

    private async Task<Product> AssertProductExists(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"The Product with Id {id} could not be found.");
        }

        return product;
    }
}
