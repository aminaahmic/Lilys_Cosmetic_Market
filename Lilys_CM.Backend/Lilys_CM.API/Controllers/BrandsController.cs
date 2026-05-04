using Lilys_CM.Application.Modules.Catalog.Brands.Commands.CreateBrand;
using Lilys_CM.Application.Modules.Catalog.Brands.Commands.DeleteBrand;
using Lilys_CM.Application.Modules.Catalog.Brands.Commands.UpdateBrand;
using Lilys_CM.Application.Modules.Catalog.Brands.Queries.GetBrandById;
using Lilys_CM.Application.Modules.Catalog.Brands.Queries.GetBrands;
using Lilys_CM.Application.Modules.Catalog.Brands.Commands.UpdateBrandLogo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lilys_CM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BrandsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BrandsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool? onlyEnabled, [FromQuery] string? search)
    {
        var result = await _mediator.Send(new GetBrandsQuery
        {
            OnlyEnabled = onlyEnabled,
            Search = search
        });

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetBrandByIdQuery
        {
            Id = id
        });

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBrandCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(id);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBrandCommand command)
    {
        command.Id = id;

        await _mediator.Send(command);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteBrandCommand
        {
            Id = id
        });

        return NoContent();
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("{id:int}/logo")]
    public async Task<IActionResult> UploadLogo(int id, IFormFile file)
    {
        if (file is null || file.Length == 0)
        {
            return BadRequest("Logo file is required.");
        }

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
        {
            return BadRequest("Only JPG, PNG and WEBP images are allowed.");
        }

        var uploadsFolder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "uploads",
            "brands"
        );

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var logoUrl = $"/uploads/brands/{fileName}";

        var command = new UpdateBrandLogoCommand
        {
            Id = id,
            LogoUrl = logoUrl
        };

        await _mediator.Send(command);

        return Ok(new
        {
            logoUrl
        });
    }
}