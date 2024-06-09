using Lerno.Shared.Models;

namespace Lerno.DataAccess.Interfaces
{
    public interface IUsersRepository
    {
        void CreateUser(User user);

        void CreateUsers(IEnumerable<User> users);

        User GetUser(string userName, string passwordHash);

        IEnumerable<User> GetAllUsers();

        void UpdateUser(User user);

        void DeleteUser(int id);

        void DeleteUsers(IEnumerable<int> ids);
    }
}
