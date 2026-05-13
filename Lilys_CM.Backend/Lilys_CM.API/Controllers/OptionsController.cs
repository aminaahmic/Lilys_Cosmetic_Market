using Lilys_CM.Application.Abstractions;
using Lilys_CM.Domain.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OptionsController : ControllerBase
{
    private readonly IAppDbContext _context;

    public OptionsController(IAppDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var options = await _context.Options
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.Name)
            .Select(x => new
            {
                x.Id,
                x.Name,
                UsageCount = _context.OptionValueEntities
                    .Count(v => !v.IsDeleted && v.OptionId == x.Id)
            })
            .ToListAsync(ct);

        return Ok(options);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOptionRequest request, CancellationToken ct)
    {
        var name = request.Name.Trim();

        var exists = await _context.Options
            .AnyAsync(x => !x.IsDeleted && x.Name.ToLower() == name.ToLower(), ct);

        if (exists)
            return BadRequest("Opcija sa ovim nazivom već postoji.");

        var option = new OptionEntity
        {
            Name = name
        };

        _context.Options.Add(option);
        await _context.SaveChangesAsync(ct);

        return Ok(option.Id);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOptionRequest request, CancellationToken ct)
    {
        var option = await _context.Options
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);

        if (option is null)
            return NotFound();

        var name = request.Name.Trim();

        var exists = await _context.Options
            .AnyAsync(x => !x.IsDeleted && x.Id != id && x.Name.ToLower() == name.ToLower(), ct);

        if (exists)
            return BadRequest("Opcija sa ovim nazivom već postoji.");

        option.Name = name;
        option.MarkUpdated();

        await _context.SaveChangesAsync(ct);

        return NoContent();
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var option = await _context.Options
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);

        if (option is null)
            return NotFound();

        var isUsed = await _context.OptionValueEntities
            .AnyAsync(x => !x.IsDeleted && x.OptionId == id, ct);

        if (isUsed)
            return BadRequest("Ne možeš obrisati opciju koja se već koristi na varijantama.");

        option.MarkDeleted();

        await _context.SaveChangesAsync(ct);

        return NoContent();
    }
}

public sealed class CreateOptionRequest
{
    public string Name { get; set; } = string.Empty;
}

public sealed class UpdateOptionRequest
{
    public string Name { get; set; } = string.Empty;
}