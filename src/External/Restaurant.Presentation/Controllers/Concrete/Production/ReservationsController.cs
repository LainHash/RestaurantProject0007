using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Restaurant.Application.Features.Production.Reservations.Commands.Create;
using Restaurant.Application.Features.Production.Reservations.Queries.GetAll;
using Restaurant.Application.Features.Production.Reservations.Queries.GetAllByWeek;
using Restaurant.Contracts.DTOs.Production.Reservations;
using Restaurant.Presentation.Controllers.Abstraction;
using System.Security.Claims;

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

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateReservationRequest request, CancellationToken cancellationToken)
        {
            Guid? userId = null; 

            if (User.Identity?.IsAuthenticated == true)
            {
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                   ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                if (Guid.TryParse(userIdString, out Guid parsedId))
                {
                    userId = parsedId; 
                }
            }

            var result = await Sender.Send(new CreateReservationCommand(request, userId), cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
