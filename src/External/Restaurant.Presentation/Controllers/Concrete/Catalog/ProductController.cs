using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Catalog.Products.Queries.GetAll;
using Restaurant.Application.Features.Catalog.Products.Queries.GetOne;
using Restaurant.Application.Messaging;
using Restaurant.Presentation.Controllers.Abstraction;

namespace Restaurant.Presentation.Controllers.Concrete.Catalog
{
    public class ProductController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductQuery query, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(query, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new GetProductByIdQuery(id), cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
