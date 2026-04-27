using MediatR;

namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Icon { get; set; }
}