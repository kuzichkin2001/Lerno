using Lerno.DataAccess.Interfaces;

namespace Lerno.DataAccess.Service.Handlers
{
    public class TeacherBusMessageHandler : IBusMessageHandler
    {
        private readonly ITeachersRepository _teachersRepository;

        public TeacherBusMessageHandler(ITeachersRepository teachersRepository)
        {
            _teachersRepository = teachersRepository;
        }

        public Task<object> HandleCommandAsync(string messageContent, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new object());
        }

        public Task HandleEventAsync(string messageContent, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
