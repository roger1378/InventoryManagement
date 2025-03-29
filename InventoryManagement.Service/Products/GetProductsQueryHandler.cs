using AutoMapper;
using InventoryManagement.Repository.Products;
using InventoryManagement.Service.Products.Dto;
using MediatR;

namespace InventoryManagement.Service.Products;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductResponseDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductResponseDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAsync();

        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }
}