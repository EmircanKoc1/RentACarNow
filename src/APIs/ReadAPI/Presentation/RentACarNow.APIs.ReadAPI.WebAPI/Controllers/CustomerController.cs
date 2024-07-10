using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Customer.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Customer.GetById;

namespace RentACarNow.APIs.ReadAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [Authorize(Policy = "ReadAPI.Customer")]

    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IMediator mediator, ILogger<CustomerController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCustomerQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdCustomerQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

    }

}
