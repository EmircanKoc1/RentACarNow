using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.CreateRental;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.DeleteRental;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.UpdateRental;

namespace RentACarNow.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [Authorize(Policy = "WriteAPI.Rental")]

    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RentalController> _logger;

        public RentalController(IMediator mediator, ILogger<RentalController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateRentalCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteRentalCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRentalCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }

}
