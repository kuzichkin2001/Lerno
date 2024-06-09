using Lerno.Shared.Models;

namespace Lerno.DataAccess.Interfaces
{
    public interface IUsersRepository
    {
        void CreateUser(User user);

        void CreateUsers(IEnumerable<User> users);

        User GetUser(string userName, string passwordHash);

        User GetUser(string userNameFilter);

        IEnumerable<User> GetAllUsers();

        IEnumerable<User> GetAllUsersFiltered(int rating);

        void UpdateUser(User user);

        void DeleteUser(int id);

        void DeleteUsers(IEnumerable<int> ids);
    }
}
