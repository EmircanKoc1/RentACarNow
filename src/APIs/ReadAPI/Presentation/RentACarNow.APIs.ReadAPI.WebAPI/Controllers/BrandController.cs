using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetById;

namespace RentACarNow.APIs.ReadAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    //[Authorize(Policy = "ReadAPI.Brand")]

    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BrandController> _logger;

        public BrandController(IMediator mediator, ILogger<BrandController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllBrandQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdBrandQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }


    }

}
