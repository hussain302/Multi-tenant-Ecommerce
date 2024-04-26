using MediatR;
using API.Contracts;
using API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Application.Modules.Users.Commands;
using Application.Modules.Users.Models;
using Application.Modules.Users.Queries;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[Authorize]
[ApiController]
public class UsersController(
    IMediator mediator) :
    ApiController(mediator)
{
    [HttpPost]
    [Route(ApiRoutes.User.PostAdmin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAdmin(
    CreateAdminCommand command,
    CancellationToken cancellationToken) =>
    command.IsValidCommand && command.IsValidEmail && command.IsValidPassword
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
                message = "Required parameters are missing."
            });

    [HttpPost]
    [Route(ApiRoutes.User.PostCustomer)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer(
    CreateCustomerCommand command,
    CancellationToken cancellationToken) =>
    command.IsValidCommand && command.IsValidEmail && command.IsValidPassword
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
                message = "Required parameters are missing."
            });

    [HttpPost]
    [Route(ApiRoutes.User.PostVendor)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateVendor(
    CreateCustomerCommand command,
    CancellationToken cancellationToken) =>
    command.IsValidCommand && command.IsValidEmail && command.IsValidPassword
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
                message = "Required parameters are missing."
            });

    [HttpPut]
    [Route(ApiRoutes.User.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(
    UpdateUserCommand command,
    CancellationToken cancellationToken) =>
    command.IsValidCommand && command.IsValidEmail
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
                message = "Required parameters are missing."
            });

    [HttpDelete]
    [Route(ApiRoutes.User.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUser(
        Guid id,
        CancellationToken cancellationToken) =>
    new DeleteUserCommand(id).IsValidCommand
        ? StatusCode(
            statusCode: StatusCodes.Status200OK,
            value: await _mediator.Send(
                    new DeleteUserCommand(id),
                    cancellationToken))
        : StatusCode(
            statusCode: StatusCodes.Status400BadRequest,
            value: new
            {
                success = false,
                message = "Parameter (Id) is required."
            });

    [HttpGet]
    [Route(ApiRoutes.User.GetById)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUser(
        Guid id,
        CancellationToken cancellationToken) =>
        new GetUserQueryById(id).IsValidCommand
        ? StatusCode(
            statusCode: StatusCodes.Status200OK,
            value: await _mediator.Send(
                    new GetUserQueryById(id),
                    cancellationToken))
        : StatusCode(
            statusCode: StatusCodes.Status400BadRequest,
            value: new
            {
                success = false,
                message = "Parameter (Id) is required."
            });

    [HttpGet]
    [Route(ApiRoutes.User.GetAll)]
    [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllUser(
        int start,
        int limit,
        string? orderBy,
        string? order,
        CancellationToken cancellationToken) =>
        new GetAllUserQuery(
            start,
            limit,
            orderBy,
            order).IsValidCommand
        ? StatusCode(
            statusCode: StatusCodes.Status200OK,
            value: await _mediator.Send(
                    new GetAllUserQuery(
                        start,
                        limit,
                        orderBy,
                        order),
                    cancellationToken))
        : StatusCode(
            statusCode: StatusCodes.Status400BadRequest,
            value: new
            {
                success = false,
                message = "Parameter (start, limit) are required."
            });
}