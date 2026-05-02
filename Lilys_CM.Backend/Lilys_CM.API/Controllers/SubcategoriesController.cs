[ApiController]
[Route("api/[controller]")]
public class SubcategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubcategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<List<SubcategoryDto>> Get([FromQuery] int categoryId)
    {
        return await _mediator.Send(new GetSubcategoriesByCategoryQuery
        {
            CategoryId = categoryId
        });
    }
}