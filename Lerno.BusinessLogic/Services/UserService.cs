using AutoMapper;
using Lerno.BusinessLogic.Bus;
using Lerno.BusinessLogic.Interfaces;
using Lerno.Shared.Commands;
using Lerno.Shared.DTOs;
using Lerno.Shared.Enums;
using Lerno.Shared.Models;

namespace Lerno.BusinessLogic.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IBusMessageQueueService busMessageService,
            IMapper mapper) : base(busMessageService, mapper) { }

        public async Task<CreateResultDTO> CreateUserAsync(UserDTO userDto, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(userDto);
            var busMessage = ConstructBusMessage(user, BusUserAction.CreateUser, BusMessageType.Command);
            busMessage.Handler = BusMessageHandlerType.User;

            return await _busMessageService.SendCommandAsync<User, CreateResultDTO>(busMessage).ConfigureAwait(false);
        }

        public Task<UserDTO> GetUserAsync(string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDTO>> GetUsersAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResultDTO> UpdateUserAsync(UserDTO userDTO, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
