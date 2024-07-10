using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.CreateCustomer;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.DeleteCustomer;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.UpdateCustomer;

namespace RentACarNow.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [Authorize(Policy = "WriteAPI.Customer")]

    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IMediator mediator, ILogger<CustomerController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

    

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCustomerCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteCustomerCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }

}
