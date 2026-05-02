namespace Lilys_CM.Application.Modules.Catalog.Subcategories.Commands.CreateSubcategory;

public class CreateSubcategoryCommand : IRequest<int>
{
    public string Name { get; set; } = default!;
    public int CategoryId { get; set; }
    public bool IsEnabled { get; set; } = true;
}