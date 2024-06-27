using Microsoft.AspNetCore.Mvc;
using RentACarNow.Application.Interfaces.Repositories.Write.EfCore;
using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;

namespace RentACarNow.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DenemeController : ControllerBase
    {
        private readonly IMongoAdminWriteRepository _writeRepository;
        private readonly IEfCoreAdminWriteRepository _efCoreRepository;
        private readonly IMongoCarWriteRepository mongoCarWriteRepository;
        public DenemeController(IMongoAdminWriteRepository writeRepository, IEfCoreAdminWriteRepository efCoreRepository, IMongoCarWriteRepository mongoCarWriteRepository)
        {
            _writeRepository = writeRepository;
            _efCoreRepository = efCoreRepository;
            this.mongoCarWriteRepository = mongoCarWriteRepository;
        }

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
