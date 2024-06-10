using Microsoft.AspNetCore.Mvc;
using Lerno.Shared.DTOs;
using Lerno.BusinessLogic.Bus;
using Lerno.BusinessLogic.Interfaces;

namespace Lerno.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsApiController : ControllerBase
    {
        private readonly ILogger<StudentsApiController> _logger;
        private readonly IStudentsService _studentsLogic;

        public StudentsApiController(ILogger<StudentsApiController> logger,
            IStudentsService studentsLogic)
        {
            _logger = logger;
            _studentsLogic = studentsLogic;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateUserAsync([FromBody] StudentDTO studentDTO)
        {
            try
            {
                _logger.LogInformation("Started sending message by bus.");
                
                return Ok(studentDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error: {message}", ex.Message);

                throw;
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetUserOfStudent(
            [FromQuery] string userName,
            [FromQuery] string password,
            CancellationToken cancellationToken)
        {
            var dto = new GetStudentDTO { UserName = userName, PasswordHash = password.GetHashCode().ToString() };
            var response = await _studentsLogic.GetUserOfStudentAsync(dto, cancellationToken)
                .ConfigureAwait(false);

            return Ok(response);
        }
    }
}
