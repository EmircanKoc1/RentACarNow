using Microsoft.AspNetCore.Mvc;
using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;

namespace RentACarNow.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DenemeController : ControllerBase
    {
        private readonly IMongoAdminWriteRepository _writeRepository;

        public DenemeController(IMongoAdminWriteRepository writeRepository)
        {
            _writeRepository = writeRepository;
        }

        [HttpGet]
        public IActionResult TryMethod()
        {

            _writeRepository.AddAsync(new Domain.Entities.MongoEntities.Admin()
            {
                Id = Guid.NewGuid(),
                Email = "emircandvb@mail.com",
                Password = "12313123"
            });

            return Ok();
        }


    }
}
