using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Territory.Areas.Queries.GetAll;
using Restaurant.Presentation.Controllers.Abstraction;

namespace Restaurant.Presentation.Controllers.Concrete.Territory
{
    public class AreasController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAreaQuery query, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(query, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
