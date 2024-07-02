﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById;
using RentACarNow.Application.Features.CQRS.Commands.Claim.CreateClaim;
using RentACarNow.Application.Features.CQRS.Commands.Claim.DeleteClaim;
using RentACarNow.Application.Features.CQRS.Commands.Claim.UpdateClaim;

namespace RentACarNow.APIs.ReadAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class ClaimController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ClaimController> _logger;

        public ClaimController(IMediator mediator, ILogger<ClaimController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] GetAllClaimQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute] GetByIdClaimQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateClaimCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteClaimCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateClaimCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }

}
