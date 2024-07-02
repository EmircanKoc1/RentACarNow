using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetById;
using RentACarNow.Application.Features.CQRS.Commands.Car.CreateCar;
using RentACarNow.Application.Features.CQRS.Commands.Car.DeleteCar;
using RentACarNow.Application.Features.CQRS.Commands.Car.UpdateCar;

namespace RentACarNow.APIs.ReadAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CarController> _logger;

        public CarController(IMediator mediator, ILogger<CarController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] GetAllCarQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute] GetByIdCarQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCarCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteCarCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCarCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }

}
