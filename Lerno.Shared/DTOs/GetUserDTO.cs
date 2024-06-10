namespace Lerno.Shared.DTOs
{
    public class GetStudentDTO
    {
        public required string UserName { get; set; }

        public required string PasswordHash { get; set; }
    }
}
