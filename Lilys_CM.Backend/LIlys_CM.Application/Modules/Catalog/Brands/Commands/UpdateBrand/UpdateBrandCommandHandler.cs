using Lilys_CM.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Brands.Commands.UpdateBrand;

public sealed class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand>
{
    private readonly IAppDbContext _context;

    public UpdateBrandCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _context.Brands
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (brand is null)
        {
            throw new Lilys_CMNotFoundException("Brand not found.");
        }

        var name = request.Name.Trim();
        var slug = string.IsNullOrWhiteSpace(request.Slug)
            ? GenerateSlug(name)
            : GenerateSlug(request.Slug);

        var nameExists = await _context.Brands
            .AnyAsync(
                x => x.Id != request.Id && x.Name.ToLower() == name.ToLower(),
                cancellationToken);

        if (nameExists)
        {
            throw new Lilys_CMConflictException("Brand with the same name already exists.");
        }

        var slugExists = await _context.Brands
            .AnyAsync(
                x => x.Id != request.Id &&
                     x.Slug != null &&
                     x.Slug.ToLower() == slug.ToLower(),
                cancellationToken);

        if (slugExists)
        {
            throw new Lilys_CMConflictException("Brand with the same slug already exists.");
        }

        brand.Name = name;
        brand.Slug = slug;
        brand.Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim();
        brand.LogoUrl = string.IsNullOrWhiteSpace(request.LogoUrl) ? null : request.LogoUrl.Trim();
        brand.IsEnabled = request.IsEnabled;

        await _context.SaveChangesAsync(cancellationToken);
    }

    private static string GenerateSlug(string value)
    {
        return value
            .Trim()
            .ToLower()
            .Replace(" ", "-")
            .Replace("č", "c")
            .Replace("ć", "c")
            .Replace("š", "s")
            .Replace("đ", "d")
            .Replace("ž", "z");
    }
}