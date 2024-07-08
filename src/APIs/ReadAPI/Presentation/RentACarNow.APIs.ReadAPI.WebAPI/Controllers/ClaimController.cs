using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById;

namespace RentACarNow.APIs.ReadAPI.WebAPI.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllClaimQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdClaimQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }


    }

}
