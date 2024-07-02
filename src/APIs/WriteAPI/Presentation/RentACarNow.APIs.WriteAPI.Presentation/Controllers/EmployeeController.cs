﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.CreateEmployee;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.DeleteEmployee;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.UpdateEmployee;
using RentACarNow.Application.Features.CQRS.Queries.Employee.GetAll;
using RentACarNow.Application.Features.CQRS.Queries.Employee.GetById;

namespace RentACarNow.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IMediator mediator, ILogger<EmployeeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] GetAllEmployeeQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute] GetByIdEmployeeQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateEmployeeCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteEmployeeCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }

}