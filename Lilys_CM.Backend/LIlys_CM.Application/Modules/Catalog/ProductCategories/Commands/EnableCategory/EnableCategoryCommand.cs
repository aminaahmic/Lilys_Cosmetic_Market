namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.EnableCategory;

public sealed class EnableCategoryCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
