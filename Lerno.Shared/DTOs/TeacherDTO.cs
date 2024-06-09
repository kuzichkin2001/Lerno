using Lerno.Shared.Models;

namespace Lerno.Shared.DTOs
{
    public class TeacherDTO
    {
        public required int Id { get; set; }

        public required int UserId { get; set; }

        public required UserDTO User { get; set; }

        public IEnumerable<StudentDTO>? Students { get; set; }
    }
}
