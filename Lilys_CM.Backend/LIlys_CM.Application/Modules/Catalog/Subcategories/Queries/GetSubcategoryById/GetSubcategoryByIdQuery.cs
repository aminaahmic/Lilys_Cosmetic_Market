using Lilys_CM.Application.Modules.Catalog.Subcategories.Common;
using MediatR;

namespace Lilys_CM.Application.Modules.Catalog.Subcategories.Queries.GetSubcategoryById;

public class GetSubcategoryByIdQuery : IRequest<SubcategoryDto>
{
    public int Id { get; set; }
}