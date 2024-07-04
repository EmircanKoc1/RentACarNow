﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.CreateCar;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.DeleteCar;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.UpdateCar;

namespace RentACarNow.WebAPI.Controllers
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