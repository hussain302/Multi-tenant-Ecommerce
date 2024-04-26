using MediatR;
using API.Contracts;
using API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Application.Modules.Roles.Models;
using Application.Modules.Roles.Queries;
using Application.Modules.Roles.Commands;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[Authorize]
[ApiController]
public class RolesController(
    IMediator mediator) :
    ApiController(mediator)
{
    [HttpPost]
    [Route(ApiRoutes.Role.Post)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRole(
    CreateRoleCommand command,
    CancellationToken cancellationToken) =>
    command.IsValidCommand
        ? StatusCode(
            statusCode: StatusCodes.Status201Created,
            value: await _mediator.Send(
                    command,
                    cancellationToken))
        : StatusCode(
            statusCode: StatusCodes.Status400BadRequest,
            value: new
            {
                success = false,
                message = "Parameters (Name and Description) are required."
            });

    [HttpPut]
    [Route(ApiRoutes.Role.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateRole(
    UpdateRoleCommand command,
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
                message = "Parameters (Id and Name) are required."
            });

    [HttpDelete]
    [Route(ApiRoutes.Role.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteRole(
        Guid id,
        CancellationToken cancellationToken) =>
    new DeleteRoleCommand(id).IsValidCommand
        ? StatusCode(
            statusCode: StatusCodes.Status200OK,
            value: await _mediator.Send(
                    new DeleteRoleCommand(id),
                    cancellationToken))
        : StatusCode(
            statusCode: StatusCodes.Status400BadRequest,
            value: new
            {
                success = false,
                message = "Parameter (Id) is required."
            });

    [HttpGet]
    [Route(ApiRoutes.Role.GetById)]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetRole(
        Guid id,
        CancellationToken cancellationToken) =>
        new GetRoleQueryById(id).IsValidCommand
        ? StatusCode(
            statusCode: StatusCodes.Status200OK,
            value: await _mediator.Send(
                    new GetRoleQueryById(id),
                    cancellationToken))
        : StatusCode(
            statusCode: StatusCodes.Status400BadRequest,
            value: new
            {
                success = false,
                message = "Parameter (Id) is required."
            });

    [HttpGet]
    [Route(ApiRoutes.Role.GetAll)]
    [ProducesResponseType(typeof(List<RoleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllRole(
        int start,
        int limit,
        string? orderBy,
        string? order,
        CancellationToken cancellationToken) =>
        new GetAllRoleQuery(
            start,
            limit,
            orderBy,
            order).IsValidCommand
        ? StatusCode(
            statusCode: StatusCodes.Status200OK,
            value: await _mediator.Send(
                    new GetAllRoleQuery(
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