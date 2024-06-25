using Microsoft.AspNetCore.Mvc;

namespace RentACarNow.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DenemeController : ControllerBase
    {

        [HttpGet]
        public IActionResult TryMethod()
        {

            return Ok();
        }


    }
}
