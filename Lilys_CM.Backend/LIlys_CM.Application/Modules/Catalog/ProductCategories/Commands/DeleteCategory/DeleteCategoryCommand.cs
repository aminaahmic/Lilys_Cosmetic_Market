using MediatR;

namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<Unit>
{
    public int Id { get; set; }
}