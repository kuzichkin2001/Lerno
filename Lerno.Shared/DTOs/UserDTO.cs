namespace Lerno.Shared.DTOs
{
    public class UserDTO
    {
        public required int Id { get; set; }

        public required string FirstName { get; set; } = string.Empty;

        public required string LastName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }
    }
}
