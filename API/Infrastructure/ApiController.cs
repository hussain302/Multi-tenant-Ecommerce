using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Infrastructure;

[ApiController]
public class ApiController : ControllerBase
{
    protected ApiController(IMediator mediator) => _mediator = mediator;

    protected IMediator _mediator { get; }

}
