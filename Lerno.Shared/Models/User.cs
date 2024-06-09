using Lerno.Shared.Enums;

namespace Lerno.Shared.Models
{
    public class User : DomainModel
    {
        public required int Id { get; set; }

        public required string FirstName { get; set; } = string.Empty;

        public required string LastName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public string[] Roles { get; set; }

        public UserStatus Status { get; set; }
        
        public virtual Student Student { get; set; }
    }
}
