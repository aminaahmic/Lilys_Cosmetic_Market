using MediatR;

namespace Lilys_CM.Application.Modules.Catalog.Subcategories.Commands.UpdateSubcategory;

public class UpdateSubcategoryCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int CategoryId { get; set; }
    public bool IsEnabled { get; set; }
}