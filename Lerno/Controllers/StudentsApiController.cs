using Microsoft.AspNetCore.Mvc;
using Lerno.Shared.DTOs;
using Lerno.BusinessLogic.Bus;

namespace Lerno.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsApiController : ControllerBase
    {
        private readonly IBusMessageQueueService _busDeliveryService;
        private readonly ILogger<StudentsApiController> _logger;

        public StudentsApiController(IBusMessageQueueService busDeliveryService,
            ILogger<StudentsApiController> logger)
        {
            _busDeliveryService = busDeliveryService;
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateUserAsync([FromBody] StudentDTO studentDTO)
        {
            try
            {
                _logger.LogInformation("Started sending message by bus.");
                
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
