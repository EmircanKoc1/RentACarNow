using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.ClaimAddedUser;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.ClaimDeletedUser;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.CreateUser;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.DeleteUser;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.UpdateUser;

namespace RentACarNow.APIs.WriteAPI.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class UserController(IMediator _mediator) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommandRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserCommandRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete]
    public async Task<IActionResult> ClaimDeleteUser([FromQuery] ClaimDeleteUserCommandRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost]
    public async Task<IActionResult> ClaimAddUser([FromBody] ClaimAddUserCommandRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

}