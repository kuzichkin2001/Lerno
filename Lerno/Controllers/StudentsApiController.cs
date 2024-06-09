using Microsoft.AspNetCore.Mvc;
using Lerno.Shared.Enums;
using Lerno.Shared.Commands;
using Lerno.Bus;
using Lerno.Shared.DTOs;

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
        public async Task<IActionResult> GetStudentByLoginName([FromBody] GetStudentDTO studentDTO)
        {
            try
            {
                _logger.LogInformation("Started sending message by bus.");
                
                var busMessage = ConstructBusMessage(GetStudentDTO, BusMessageType.Command);
                busMessage.Action = BusStudentAction.GetStudent;
                busMessage.Handler = BusMessageHandlerType.Student;

                var result = await _busDeliveryService.SendCommandAsync<GetStudentDTO, UserDTO>(busMessage);
                
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
