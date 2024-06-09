using Lerno.BusinessLogic.Interfaces;
using Lerno.Shared.Commands;
using Lerno.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Lerno.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersApiController> _logger;

        public UsersApiController(IUserService userService,
            ILogger<UsersApiController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Route("init/{message}")]
        public IActionResult SendMessageToBus(string message)
        {
            try
            {   
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error yopta: {message}", ex.Message);

                throw;
            }
        }
    }
}
