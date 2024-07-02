using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarNow.Application.Features.CQRS.Queries.Admin.GetAll;
using RentACarNow.Application.Interfaces.Repositories.Write.EfCore;
using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;

namespace RentACarNow.APIs.ReadAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DenemeController : ControllerBase
    {
        private readonly IMongoAdminWriteRepository _writeRepository;
        private readonly IEfCoreAdminWriteRepository _efCoreRepository;
        private readonly IMongoCarWriteRepository mongoCarWriteRepository;
        private readonly IMediator _mediator;
        public DenemeController(IMongoAdminWriteRepository writeRepository, IEfCoreAdminWriteRepository efCoreRepository, IMongoCarWriteRepository mongoCarWriteRepository, IMediator mediator)
        {
            _writeRepository = writeRepository;
            _efCoreRepository = efCoreRepository;
            this.mongoCarWriteRepository = mongoCarWriteRepository;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> TryMediatr()
            => Ok(await _mediator.Send(new GetAllAdminQueryRequest()));

        [HttpGet]
        public async Task<IActionResult> TryMethod()
        {

            await mongoCarWriteRepository.AddAsync(new Domain.Entities.MongoEntities.Car
            {
                Brand = new Domain.Entities.MongoEntities.Brand
                {
                    Name = "mercedes "
                },
                Color = "pink",
                Modal = "ne bilem modeli"
            });

            await mongoCarWriteRepository.AddAsync(new Domain.Entities.MongoEntities.Car
            {

                Color = "pink",
                Modal = "ne bilem modeli"
            });


            return Ok();
        }


    }
}
