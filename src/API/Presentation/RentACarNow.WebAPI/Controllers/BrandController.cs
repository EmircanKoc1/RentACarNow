using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.Application.Features.CQRS.Commands.Brand.CreateBrand;
using RentACarNow.Application.Features.CQRS.Commands.Brand.DeleteBrand;
using RentACarNow.Application.Features.CQRS.Commands.Brand.UpdateBrand;
using RentACarNow.Application.Features.CQRS.Queries.Brand.GetAll;
using RentACarNow.Application.Features.CQRS.Queries.Brand.GetById;

namespace RentACarNow.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
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
        public async Task<IActionResult> GetAll([FromRoute] GetAllBrandQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute] GetByIdBrandQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBrandCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteBrandCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBrandCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }

}
