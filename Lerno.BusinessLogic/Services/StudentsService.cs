using AutoMapper;
using Lerno.BusinessLogic.Bus;
using Lerno.BusinessLogic.Interfaces;
using Lerno.Shared.Commands;
using Lerno.Shared.DTOs;
using Lerno.Shared.Enums;
using Lerno.Shared.Models;

namespace Lerno.BusinessLogic.Services
{
    public class StudentsService : BaseService, IStudentsService
    {
        public StudentsService(IBusMessageQueueService busMessageQueueService,
            IMapper mapper) : base(busMessageQueueService, mapper)
        { }

        public async Task<UserDTO> GetUserOfStudentAsync(GetStudentDTO getStudentDto, CancellationToken cancellationToken)
        {
            var busMessage = ConstructBusMessage(getStudentDto, BusStudentAction.GetStudent, BusMessageType.Command);
            busMessage.Handler = BusMessageHandlerType.Student;

            var response = await _busMessageService.SendCommandAsync<GetStudentDTO, User>(busMessage, cancellationToken);

            if (response is null) return null;

            var resultDto = _mapper.Map<UserDTO>(response);

            return resultDto;
        }
    }
}
