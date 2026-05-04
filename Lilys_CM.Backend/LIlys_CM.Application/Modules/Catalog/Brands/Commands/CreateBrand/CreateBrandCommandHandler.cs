using Lilys_CM.Application.Abstractions;
using Lilys_CM.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Brands.Commands.CreateBrand;

public sealed class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, int>
{
    private readonly IAppDbContext _context;

    public CreateBrandCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var name = request.Name.Trim();
        var slug = string.IsNullOrWhiteSpace(request.Slug)
            ? GenerateSlug(name)
            : GenerateSlug(request.Slug);

        var nameExists = await _context.Brands
            .AnyAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);

        if (nameExists)
        {
            throw new Lilys_CMConflictException("Brand with the same name already exists.");
        }

        var slugExists = await _context.Brands
            .AnyAsync(x => x.Slug != null && x.Slug.ToLower() == slug.ToLower(), cancellationToken);

        if (slugExists)
        {
            throw new Lilys_CMConflictException("Brand with the same slug already exists.");
        }

        var brand = new BrandEntity
        {
            Name = name,
            Slug = slug,
            Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim(),
            LogoUrl = string.IsNullOrWhiteSpace(request.LogoUrl) ? null : request.LogoUrl.Trim(),
            IsEnabled = request.IsEnabled
        };

        _context.Brands.Add(brand);
        await _context.SaveChangesAsync(cancellationToken);

        return brand.Id;
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