using AutoMapper;
using FluentValidation;
using InventoryManagement.Repository.Model;
using InventoryManagement.Repository.Products;
using InventoryManagement.Service.Products.Dto;
using MediatR;

namespace InventoryManagement.Service.Products;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponseDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProductUpdateRequestDto> _validator;

    public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IValidator<ProductUpdateRequestDto> validator)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ProductResponseDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request.ProductUpdateRequestDto);

        var product = await AssertProductExists(request.ProductUpdateRequestDto.Id);
        
        var modelProduct = _mapper.Map(request.ProductUpdateRequestDto, product);
        await _productRepository.UpdateAsync(modelProduct);

        return _mapper.Map<ProductResponseDto>(modelProduct);
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
