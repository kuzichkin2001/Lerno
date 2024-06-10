using Lerno.Shared.DTOs;

namespace Lerno.BusinessLogic.Interfaces
{
    public interface IStudentsService
    {
        Task<UserDTO> GetUserOfStudentAsync(
            GetStudentDTO getStudentDto,
            CancellationToken cancellationToken);
    }
}
