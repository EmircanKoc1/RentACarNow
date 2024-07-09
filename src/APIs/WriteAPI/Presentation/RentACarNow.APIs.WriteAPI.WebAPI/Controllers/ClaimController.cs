using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.AddClaimToAdmin;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.AddClaimToEmployee;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.CreateClaim;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaim;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaimFromAdmin;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaimFromCustomer;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaimFromEmployee;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.UpdateClaim;

namespace RentACarNow.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class ClaimController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ClaimController> _logger;

        public ClaimController(IMediator mediator, ILogger<ClaimController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateClaimCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> AddClaimToAdmin([FromBody] AddClaimToAdminCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> AddClaimToEmployee([FromBody] AddClaimToEmployeeCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> AddClaimToCustomer([FromBody] AddClaimToEmployeeCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClaimFromEmployee([FromBody] DeleteClaimFromEmployeeCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteClaimFromCustomer([FromBody] DeleteClaimFromCustomerCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteClaimFromAdmin([FromBody] DeleteClaimFromAdminCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }




        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteClaimCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateClaimCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }

}
