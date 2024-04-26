using MediatR;
using API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[Authorize]
[ApiController]
public class CategoriesController(
    IMediator mediator) :
    ApiController(mediator)
{

}
