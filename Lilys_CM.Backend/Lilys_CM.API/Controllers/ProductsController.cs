using Lilys_CM.Application.Modules.Catalog.Products.Commands.CreateProduct;
using Lilys_CM.Application.Modules.Catalog.Products.Commands.DeleteProduct;
using Lilys_CM.Application.Modules.Catalog.Products.Commands.AdjustStock;
using Lilys_CM.Application.Modules.Catalog.Products.Commands.UpdateProduct;
using Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductById;
using Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductFilterOptions;
using Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProducts;
using Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductStockMovements;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteProductCommand { Id = id });
        return NoContent();
    }

    [Authorize]
    [HttpPut("{id:int}/stock/adjust")]
    public async Task<IActionResult> AdjustStock(int id, [FromBody] AdjustProductStockCommand command)
    {
        command.ProductId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpGet("{id:int}/stock-movements")]
    public async Task<IActionResult> GetStockMovements(int id, [FromQuery] GetProductStockMovementsQuery query)
    {
        query.ProductId = id;
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
