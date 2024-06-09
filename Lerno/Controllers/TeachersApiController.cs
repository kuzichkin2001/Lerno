using Microsoft.AspNetCore.Mvc;

namespace Lerno.Controllers
{
    [ApiController]
    [Route("api/teachers")]
    public class TeachersApiController : ControllerBase
    {
        [HttpGet]
        [Route("{userName}")]
        public IActionResult GetTeacher(string userName)
        {
            return Ok();
        }
    }
}
