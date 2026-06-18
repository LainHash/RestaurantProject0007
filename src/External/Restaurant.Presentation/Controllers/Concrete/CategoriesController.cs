using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;
using Restaurant.Presentation.Controllers.Abstraction;

namespace Restaurant.Presentation.Controllers.Concrete;

public class CategoriesController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCategoryQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        
        return StatusCode(result.StatusCode, result);
    }
}
