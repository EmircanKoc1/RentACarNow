﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetById;

namespace RentACarNow.APIs.ReadAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IMediator mediator, ILogger<AdminController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] GetAllAdminQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute] GetByIdAdminQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }




    }
}
