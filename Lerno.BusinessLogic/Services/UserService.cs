using Lerno.BusinessLogic.Interfaces;
using Lerno.Shared.DTOs;

namespace Lerno.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        public UserService() { }

        public IEnumerable<UserDTO> GetUsers()
        {
            var result = new List<UserDTO>();
 
            return result;
        }

        public IEnumerable<UserDTO> GetUsers(int start, int from)
        {
            var result = new List<UserDTO>();

            return result;
        }
    }
}
