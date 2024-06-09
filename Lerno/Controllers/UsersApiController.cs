using Lerno.Bus;
using Lerno.BusinessLogic.Interfaces;
using Lerno.Shared.Commands;
using Lerno.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Lerno.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IBusMessageQueueService _busService;
        private readonly ILogger<UsersApiController> _logger;

        public UsersApiController(IUserService userService,
            IBusMessageQueueService busService,
            ILogger<UsersApiController> logger)
        {
            _userService = userService;
            _busService = busService;
            _logger = logger;
        }

        [HttpPost]
        [Route("init/{message}")]
        public IActionResult SendMessageToBus(string message)
        {
            try
            {
                _logger.LogInformation("Sending some message yopta.");

                var busMessage = ConstructBusMessage(message, BusMessageType.Event);
                busMessage.Action = BusUserAction.GetUser;

                _busService.SendEvent(busMessage);
                
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error yopta: {message}", ex.Message);

                throw;
            }
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAllUsers([FromQuery] int from, [FromQuery] int to)
        {
            var users = _userService.GetUsers(from, to);

            return Ok(users);
        }
    }
}
