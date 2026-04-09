using Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProducts;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetProductsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}