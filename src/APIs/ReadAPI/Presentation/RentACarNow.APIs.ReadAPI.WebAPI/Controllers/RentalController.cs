using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetById;
using RentACarNow.Application.Features.CQRS.Commands.Rental.CreateRental;
using RentACarNow.Application.Features.CQRS.Commands.Rental.DeleteRental;
using RentACarNow.Application.Features.CQRS.Commands.Rental.UpdateRental;

namespace RentACarNow.APIs.ReadAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RentalController> _logger;

        public RentalController(IMediator mediator, ILogger<RentalController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] GetAllRentalQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute] GetByIdRentalQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateRentalCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRentalCommandRequest request)
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
