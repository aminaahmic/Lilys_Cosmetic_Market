using FluentValidation;

namespace Lilys_CM.Application.Modules.Catalog.Products.Commands.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Sku)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.Slug)
            .MaximumLength(160)
            .When(x => !string.IsNullOrWhiteSpace(x.Slug));

        RuleFor(x => x.ImageUrl)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl));

        RuleFor(x => x.ShortDescription)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.ShortDescription));

        RuleFor(x => x.Description)
            .MaximumLength(4000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.Ingredients)
            .MaximumLength(4000)
            .When(x => !string.IsNullOrWhiteSpace(x.Ingredients));

        RuleFor(x => x.HowToUse)
            .MaximumLength(2000)
            .When(x => !string.IsNullOrWhiteSpace(x.HowToUse));

        RuleFor(x => x.Benefits)
            .MaximumLength(2000)
            .When(x => !string.IsNullOrWhiteSpace(x.Benefits));

        RuleFor(x => x.Brand)
            .MaximumLength(120)
            .When(x => !string.IsNullOrWhiteSpace(x.Brand));

        RuleFor(x => x.Size)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.Size));

        RuleFor(x => x.CountryOfOrigin)
            .MaximumLength(120)
            .When(x => !string.IsNullOrWhiteSpace(x.CountryOfOrigin));

        RuleFor(x => x.Barcode)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.Barcode));

        RuleFor(x => x.SeoTitle)
            .MaximumLength(200)
            .When(x => !string.IsNullOrWhiteSpace(x.SeoTitle));

        RuleFor(x => x.SeoDescription)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.SeoDescription));

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.CompareAtPrice)
            .GreaterThanOrEqualTo(0)
            .When(x => x.CompareAtPrice.HasValue);

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0);
    }
}