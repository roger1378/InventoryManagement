using FluentValidation;
using InventoryManagement.Service.Products.Dto;

namespace InventoryManagement.Service.Products.Validators;

public class ProductUpdateRequestDtoValidator : AbstractValidator<ProductUpdateRequestDto>
{
    public ProductUpdateRequestDtoValidator()
    {
        RuleFor(dto => dto).NotNull();

        RuleFor(dto => dto.Id)
            .NotEmpty()
            .NotNull();

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
