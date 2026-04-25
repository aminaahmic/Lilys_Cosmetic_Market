using Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.CreateCategory;
using Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.DeleteCategory;
using Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.DisableCategory;
using Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.EnableCategory;
using Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.UpdateCategory;
using Lilys_CM.Application.Modules.Catalog.ProductCategories.Queries.GetProductCategoryById;
using Lilys_CM.Application.Modules.Catalog.ProductCategories.Queries.GetProductCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lilys_CM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetProductCategoriesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetProductCategoryByIdQuery { Id = id });
        return Ok(result);
    }

[Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

  [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

   [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteCategoryCommand { Id = id });
        return NoContent();
    }

   [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}/enable")]
    public async Task<IActionResult> Enable(int id)
    {
        await _mediator.Send(new EnableCategoryCommand { Id = id });
        return NoContent();
    }

   [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}/disable")]
    public async Task<IActionResult> Disable(int id)
    {
        await _mediator.Send(new DisableCategoryCommand { Id = id });
        return NoContent();
    }
}
