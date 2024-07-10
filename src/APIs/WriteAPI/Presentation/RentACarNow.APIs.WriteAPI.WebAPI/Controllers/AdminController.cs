using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.CreateAdmin;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.DeleteAdmin;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.UpdateAdmin;
using System.Text.Json.Serialization;

namespace RentACarNow.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [Authorize(Policy = "WriteAPI.Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IMediator mediator, ILogger<AdminController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

      
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateAdminCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteAdminCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAdminCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }



    }
}
