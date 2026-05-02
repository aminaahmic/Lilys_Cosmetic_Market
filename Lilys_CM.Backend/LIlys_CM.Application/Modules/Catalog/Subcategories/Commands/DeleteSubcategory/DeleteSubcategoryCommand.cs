using MediatR;

namespace Lilys_CM.Application.Modules.Catalog.Subcategories.Commands.DeleteSubcategory;

public class DeleteSubcategoryCommand : IRequest
{
    public int Id { get; set; }
}