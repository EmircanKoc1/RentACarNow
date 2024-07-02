using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Feature.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Feature.GetById;
using RentACarNow.Application.Features.CQRS.Commands.Feature.CreateFeature;
using RentACarNow.Application.Features.CQRS.Commands.Feature.DeleteFeature;
using RentACarNow.Application.Features.CQRS.Commands.Feature.UpdateFeature;

namespace RentACarNow.APIs.ReadAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
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
        public async Task<IActionResult> GetAll([FromRoute] GetAllFeatureQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute] GetByIdFeatureQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateFeatureCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteFeatureCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFeatureCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }

}
