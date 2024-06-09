namespace Lerno.Shared.Models
{
    public class Teacher : DomainModel
    {
        public required int Id { get; set; }

        public required int UserId { get; set; }

        public required virtual User User { get; set; }

        public virtual IEnumerable<Student>? Students { get; set; }
    }
}
