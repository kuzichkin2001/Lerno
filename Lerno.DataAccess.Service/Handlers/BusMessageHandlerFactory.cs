using Lerno.DataAccess.DbContexts;
using Lerno.DataAccess.Interfaces;
using Lerno.Shared.Enums;

namespace Lerno.DataAccess.Service.Handlers
{
    public class BusMessageHandlerFactory : IBusMessageHandlerFactory
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsersRepository _usersRepository;
        private readonly IStudentsRepository _studentsRepository;
        private readonly ITeachersRepository _teachersRepository;

        public BusMessageHandlerFactory(IUnitOfWork unitOfWork,
            IUsersRepository usersRepository,
            IStudentsRepository studentsRepository,
            ITeachersRepository teachersRepository)
        {
            _unitOfWork = unitOfWork;
            _usersRepository = usersRepository;
            _studentsRepository = studentsRepository;
            _teachersRepository = teachersRepository;
        }

        public IBusMessageHandler Create(BusMessageHandlerType handlerType)
        {
            return handlerType switch
            {
                BusMessageHandlerType.User => new UsersBusMessageHandler
            }
        }
    }
}
