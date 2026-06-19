using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Catalog.Categories.Commands.Create;
using Restaurant.Application.Features.Catalog.Categories.Commands.Delete;
using Restaurant.Application.Features.Catalog.Categories.Commands.Restore;
using Restaurant.Application.Features.Catalog.Categories.Commands.Update;
using Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;
using Restaurant.Contracts.DTOs.Catalog.Categories;
using Restaurant.Presentation.Controllers.Abstraction;

namespace Restaurant.Presentation.Controllers.Concrete.Catalog;

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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellation)
    {
        var result = await Sender.Send(new UpdateCategoryCommand(id, request), cancellation);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellation)
    {
        var result = await Sender.Send(new DeleteCategoryCommand(id), cancellation);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPatch("{id}/restore")]
    public async Task<IActionResult> Restore([FromRoute] Guid id, CancellationToken cancellation)
    {
        var result = await Sender.Send(new RestoreCategoryCommand(id), cancellation);
        return StatusCode(result.StatusCode, result);
    }
}
