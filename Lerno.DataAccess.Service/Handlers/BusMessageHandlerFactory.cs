using Lerno.DataAccess.DbContexts;
using Lerno.DataAccess.Interfaces;
using Lerno.Shared.Enums;

namespace Lerno.DataAccess.Service.Handlers
{
    public class BusMessageHandlerFactory : IBusMessageHandlerFactory
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IStudentsRepository _studentsRepository;
        private readonly ITeachersRepository _teachersRepository;

        public BusMessageHandlerFactory(IUsersRepository usersRepository,
            IStudentsRepository studentsRepository,
            ITeachersRepository teachersRepository)
        {
            _usersRepository = usersRepository;
            _studentsRepository = studentsRepository;
            _teachersRepository = teachersRepository;
        }

        public IBusMessageHandler Create(BusMessageHandlerType handlerType)
        {
            return handlerType switch
            {
                BusMessageHandlerType.User => new UsersBusMessageHandler(_usersRepository),
                BusMessageHandlerType.Student => new StudentsBusMessageHandler(_studentsRepository),
                BusMessageHandlerType.Teacher => new TeacherBusMessageHandler(_teachersRepository),
            };
        }
    }
}
