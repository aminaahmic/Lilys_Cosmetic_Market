using Lilys_CM.Application.Modules.Catalog.ProductVariants.Commands.CreateProductVariant;
using Lilys_CM.Application.Modules.Catalog.ProductVariants.Commands.DeleteProductVariant;
using Lilys_CM.Application.Modules.Catalog.ProductVariants.Queries.GetProductVariants;
using Lilys_CM.Application.Modules.Catalog.ProductVariants.Commands.UpdateProductVariant;
[ApiController]
[Route("api/products/{productId:int}/variants")]
public sealed class ProductVariantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductVariantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get(int productId)
    {
        var result = await _mediator.Send(new GetProductVariantsQuery
        {
            ProductId = productId
        });

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(
        int productId,
        [FromBody] CreateProductVariantCommand command)
    {
        command.ProductId = productId;

        var id = await _mediator.Send(command);

        return Ok(id);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{variantId:int}")]
    public async Task<IActionResult> Update(
        int productId,
        int variantId,
        [FromBody] UpdateProductVariantCommand command)
    {
        command.ProductId = productId;
        command.VariantId = variantId;

        await _mediator.Send(command);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{variantId:int}")]
    public async Task<IActionResult> Delete(int productId, int variantId)
    {
        await _mediator.Send(new DeleteProductVariantCommand
        {
            ProductId = productId,
            VariantId = variantId
        });

        return NoContent();
    }
}