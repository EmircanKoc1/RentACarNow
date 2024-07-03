using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.CreateEmployee;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.DeleteEmployee;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.UpdateEmployee;

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
