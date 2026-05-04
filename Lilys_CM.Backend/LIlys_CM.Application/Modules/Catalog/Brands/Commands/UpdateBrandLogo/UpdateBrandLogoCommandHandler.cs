using Lilys_CM.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Brands.Commands.UpdateBrandLogo;

public sealed class UpdateBrandLogoCommandHandler : IRequestHandler<UpdateBrandLogoCommand>
{
    private readonly IAppDbContext _context;

    public UpdateBrandLogoCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateBrandLogoCommand request, CancellationToken cancellationToken)
    {
        var brand = await _context.Brands
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (brand is null)
        {
            throw new Lilys_CMNotFoundException("Brand not found.");
        }

        brand.LogoUrl = request.LogoUrl;

        await _context.SaveChangesAsync(cancellationToken);
    }
}