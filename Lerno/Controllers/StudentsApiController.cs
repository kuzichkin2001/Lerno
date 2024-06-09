using Microsoft.AspNetCore.Mvc;
using Lerno.Shared.Enums;
using Lerno.Shared.Commands;
using Lerno.Bus;

namespace Lerno.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsApiController : BaseApiController
    {
        private readonly IBusMessageQueueService _busDeliveryService;
        private readonly ILogger<StudentsApiController> _logger;

        public StudentsApiController(IBusMessageQueueService busDeliveryService,
            ILogger<StudentsApiController> logger)
        {
            _busDeliveryService = busDeliveryService;
            _logger = logger;
        }

        [HttpGet]
        [Route("{userName}")]
        public IActionResult GetStudentByLoginName(string userName)
        {
            try
            {
                _logger.LogInformation("Started sending message by bus.");
                
                var busMessage = ConstructBusMessage(userName, BusMessageType.Command);
                busMessage.Action = BusStudentAction.GetStudent;
                busMessage.Handler = BusMessageHandlerType.Student;

                var result = _busDeliveryService.SendCommand<string, string>(busMessage);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error: {message}", ex.Message);

                throw;
            }
        }
    }
}
