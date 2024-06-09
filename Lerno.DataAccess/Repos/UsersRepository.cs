using Lerno.DataAccess.DbContexts;
using Lerno.DataAccess.Interfaces;
using Lerno.Shared.Models;

namespace Lerno.DataAccess.Repos
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers(int from, int to)
        {
            throw new NotImplementedException();
        }
    }
}
