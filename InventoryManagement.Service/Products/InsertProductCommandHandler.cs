using AutoMapper;
using FluentValidation;
using InventoryManagement.Repository.Model;
using InventoryManagement.Repository.Products;
using InventoryManagement.Service.Products.Dto;
using MediatR;

namespace InventoryManagement.Service.Products;

public class InsertProductCommandHandler : IRequestHandler<InsertProductCommand, ProductResponseDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProductCreateRequestDto> _validator;

    public InsertProductCommandHandler(
        IProductRepository productRepository,
        IMapper mapper,
        IValidator<ProductCreateRequestDto> validator)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ProductResponseDto> Handle(InsertProductCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request.ProductCreateRequestDto);

        var product = _mapper.Map<Product>(request.ProductCreateRequestDto);
        product.Id = Guid.NewGuid();

        var newProduct = await _productRepository.InsertAsync(product);

        return _mapper.Map<ProductResponseDto>(newProduct);
    }
}
