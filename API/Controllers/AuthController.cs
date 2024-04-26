using MediatR;
using API.Contracts;
using API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Application.Modules.Auth.Commands;

namespace API.Controllers;

[ApiController]
public class AuthController(
    IMediator mediator) :
    ApiController(mediator)
{
    [HttpPost]
    [Route(ApiRoutes.Auth.Login)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginUser(
    LoginCommand command,
    CancellationToken cancellationToken) =>
    command.IsValidCommand
    ? StatusCode(
        statusCode: StatusCodes.Status200OK,
        value: await _mediator.Send(
                command,
                cancellationToken))
    : StatusCode(
        statusCode: StatusCodes.Status400BadRequest,
        value: new
        {
            success = false,
            message = "Required parameters are null/empty."
        });
}
