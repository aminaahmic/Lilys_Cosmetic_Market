using MediatR;

namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<int>
{
    public string Name { get; set; } = default!;
    public string? Icon { get; set; }
    public bool IsEnabled { get; set; } = true;
}