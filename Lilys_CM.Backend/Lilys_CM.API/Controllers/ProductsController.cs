using Lilys_CM.Application.Modules.Catalog.Products.Commands.CreateProduct;
using Lilys_CM.Application.Modules.Catalog.Products.Commands.DeleteProduct;
using Lilys_CM.Application.Modules.Catalog.Products.Commands.AdjustStock;
using Lilys_CM.Application.Modules.Catalog.Products.Commands.UpdateProduct;
using Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductById;
using Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductFilterOptions;
using Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProducts;
using Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductStockMovements;
using Microsoft.AspNetCore.Authorization;
using Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductSearchSuggestions;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetProductsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("filter-options")]
    public async Task<IActionResult> GetFilterOptions([FromQuery] GetProductFilterOptionsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery { Id = id });
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
    {
        var updatedCommand = new UpdateProductCommand
        {
            Id = id,

            Name = command.Name,
            Sku = command.Sku,
            Slug = command.Slug,

            ImageUrl = command.ImageUrl,
            ShortDescription = command.ShortDescription,
            Description = command.Description,
            Ingredients = command.Ingredients,
            HowToUse = command.HowToUse,
            Benefits = command.Benefits,

            Brand = command.Brand,
            BrandId = command.BrandId,
            Size = command.Size,
            CountryOfOrigin = command.CountryOfOrigin,
            Barcode = command.Barcode,

            Price = command.Price,
            CompareAtPrice = command.CompareAtPrice,
            StockQuantity = command.StockQuantity,

            IsEnabled = command.IsEnabled,
            IsFeatured = command.IsFeatured,

            SeoTitle = command.SeoTitle,
            SeoDescription = command.SeoDescription,

            CategoryId = command.CategoryId,
            SubcategoryId = command.SubcategoryId
        };

        await _mediator.Send(updatedCommand);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteProductCommand { Id = id });
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}/stock/adjust")]
    public async Task<IActionResult> AdjustStock(int id, [FromBody] AdjustProductStockCommand command)
    {
        command.ProductId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}/stock-movements")]
    public async Task<IActionResult> GetStockMovements(int id, [FromQuery] GetProductStockMovementsQuery query)
    {
        query.ProductId = id;
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpGet("search-suggestions")]
    public async Task<IActionResult> GetSearchSuggestion([FromQuery] GetProductSearchSuggestionsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
