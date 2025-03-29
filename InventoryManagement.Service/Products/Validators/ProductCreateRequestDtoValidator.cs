using FluentValidation;
using InventoryManagement.Service.Products.Dto;

namespace OInventoryManagement.Service.Products.Validators;

public class ProductCreateRequestDtoValidator : AbstractValidator<ProductCreateRequestDto>
{
    public ProductCreateRequestDtoValidator()
    {
        RuleFor(dto => dto).NotNull();

        RuleFor(dto => dto.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(128);

        RuleFor(dto => dto.Price)
            .GreaterThan(0);

        RuleFor(dto => dto.Stock)
            .GreaterThan(0);
    }
}
