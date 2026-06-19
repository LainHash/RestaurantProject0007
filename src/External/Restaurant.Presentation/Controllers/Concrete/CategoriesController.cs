using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Catalog.Categories.Commands.Create;
using Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;
using Restaurant.Contracts.DTOs.Catalog.Categories;
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request, CancellationToken cancellation)
    {
        var result = await Sender.Send(new CreateCategoryCommand(request), cancellation);
        return StatusCode(result.StatusCode, result);
    }
}
