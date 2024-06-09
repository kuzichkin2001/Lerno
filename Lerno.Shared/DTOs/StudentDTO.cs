namespace Lerno.Shared.DTOs
{
    public class StudentDTO
    {
        public required int Id { get; set; }

        public required int UserId { get; set; }

        public required UserDTO User { get; set; }

        public TeacherDTO? Teacher { get; set; }
    }
}
