//Amina
using Microsoft.AspNetCore.Mvc;
namespace Lilys_CM.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var products = new List<object>
        {
            new {
                id = 1,
                name = "Laptop",
                price = 1500,
                stockQuantity = 10,
                categoryName = "Elektronika",
                isEnabled = true
            },
            new {
                id = 2,
                name = "Telefon",
                price = 800,
                stockQuantity = 5,
                categoryName = "Mobilni uređaji",
                isEnabled = true
            }
        };

        var result = new
        {
            items = products,
            totalCount = products.Count
        };

        return Ok(result);
    }
}