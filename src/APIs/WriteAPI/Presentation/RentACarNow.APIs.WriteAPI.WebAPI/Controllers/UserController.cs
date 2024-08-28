using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.IdentityModel.Tokens;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.CreateClaim;
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
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUserCommandRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(DeleteUserCommandRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete]
    public async Task<IActionResult> ClaimDeleteUser(ClaimDeleteUserCommandRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost]
    public async Task<IActionResult> ClaimCreateUser(ClaimAddUserCommandRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

}