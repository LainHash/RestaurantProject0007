using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Production.Reservations.Queries.GetAll;
using Restaurant.Application.Features.Production.Reservations.Queries.GetAllByWeek;
using Restaurant.Presentation.Controllers.Abstraction;

namespace Restaurant.Presentation.Controllers.Concrete.Production
{
    public class ReservationsController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllReservationQuery query, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(query, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{weekStart}")]
        public async Task<IActionResult> GetAllByWeek([FromRoute] DateTime weekStart, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new GetAllReservationByWeekQuery(weekStart), cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
