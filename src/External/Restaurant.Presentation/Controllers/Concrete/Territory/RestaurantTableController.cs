using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Territory.Areas.Queries.GetAll;
using Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetAll;
using Restaurant.Presentation.Controllers.Abstraction;

namespace Restaurant.Presentation.Controllers.Concrete.Territory
{
    public class RestaurantTableController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllRestaurantTableQuery query, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(query, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
