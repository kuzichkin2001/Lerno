using Lerno.DataAccess.Interfaces;

namespace Lerno.DataAccess.Service.Handlers
{
    public class UsersBusMessageHandler : IBusMessageHandler
    {
        private readonly IUsersRepository _usersRepository;

        public UsersBusMessageHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Task<object> HandleCommandAsync(string messageContent, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task HandleEventAsync(string messageContent, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
