using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Authentication.Commands.Login;
using Restaurant.Application.Features.Authentication.Commands.Register;
using Restaurant.Application.Features.Authentication.Commands.VerifyEmail;
using Restaurant.Contracts.DTOs.Authentication;

namespace Restaurant.Presentation.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterCommand(request);
        var result = await _sender.Send(command, cancellationToken);
        
        if (result.IsSucceed)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSucceed)
        {
            return Ok(result);
        }

        return StatusCode(StatusCodes.Status401Unauthorized, result);
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request, CancellationToken cancellationToken)
    {
        var command = new VerifyEmailCommand(request);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSucceed)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}
