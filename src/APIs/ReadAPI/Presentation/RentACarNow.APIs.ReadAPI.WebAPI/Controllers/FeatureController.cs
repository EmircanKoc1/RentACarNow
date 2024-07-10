using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Feature.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Feature.GetById;

namespace RentACarNow.APIs.ReadAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [Authorize(Policy = "ReadAPI.Feature")]

    public class FeatureController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FeatureController> _logger;

        public FeatureController(IMediator mediator, ILogger<FeatureController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllFeatureQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdFeatureQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }


    }

}
