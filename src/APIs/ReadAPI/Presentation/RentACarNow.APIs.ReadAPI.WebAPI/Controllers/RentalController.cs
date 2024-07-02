using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetById;

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

      
    }

}
