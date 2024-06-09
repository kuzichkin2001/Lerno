namespace Lerno.Shared.Models
{
    public class Student : DomainModel
    {
        public required int Id { get; set; }

        public required int UserId { get; set; }

        public required virtual User User { get; set; }

        public virtual Teacher? Teacher { get; set; }
    }
}
