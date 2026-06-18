using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;
using Restaurant.Presentation.Abstraction;

namespace Restaurant.Presentation.Concrete;

public class CategoriesController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllCategoryQuery();
        var result = await Sender.Send(query, cancellationToken);
        
        return Ok(result);
    }
}
