namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.DisableCategory;

public sealed class DisableCategoryCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
