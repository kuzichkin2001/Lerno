using Lerno.Shared.DTOs;

namespace Lerno.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsersAsync(CancellationToken cancellationToken);

        Task<UserDTO> GetUserAsync(string userName, CancellationToken cancellationToken);

        Task<CreateResultDTO> CreateUserAsync(UserDTO userDTO, CancellationToken cancellationToken);

        Task<UpdateResultDTO> UpdateUserAsync(UserDTO userDTO, CancellationToken cancellationToken);
    }
}
