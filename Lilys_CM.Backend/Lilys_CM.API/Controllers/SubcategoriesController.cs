using Lilys_CM.Application.Modules.Catalog.Subcategories.Commands.CreateSubcategory;
using Lilys_CM.Application.Modules.Catalog.Subcategories.Commands.DeleteSubcategory;
using Lilys_CM.Application.Modules.Catalog.Subcategories.Commands.UpdateSubcategory;
using Lilys_CM.Application.Modules.Catalog.Subcategories.Queries.GetSubcategories;
using Lilys_CM.Application.Modules.Catalog.Subcategories.Queries.GetSubcategoryById;

namespace Lilys_CM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubcategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubcategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetSubcategoriesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetSubcategoryByIdQuery { Id = id });
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSubcategoryCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSubcategoryCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteSubcategoryCommand { Id = id });
        return NoContent();
    }
}