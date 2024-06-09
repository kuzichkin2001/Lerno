using Lerno.Shared.DTOs;

namespace Lerno.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetUsers();

        IEnumerable<UserDTO> GetUsers(int start, int from);
    }
}
