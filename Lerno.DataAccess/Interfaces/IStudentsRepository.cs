using Lerno.Shared.Models;

namespace Lerno.DataAccess.Interfaces
{
    public interface IStudentsRepository
    {
        Task CreateStudentAsync(Student student, CancellationToken cancellationToken);

        Task CreateStudentsAsync(IEnumerable<Student> students, CancellationToken cancellationToken);

        Task<User> GetUserOfStudentAsync(string userName, string passwordHash, CancellationToken cancellationToken);

        Task<IEnumerable<Student>> GetAllStudents(CancellationToken cancellationToken);

        Task UpdateStudent(Student student, CancellationToken cancellationToken);

        Task DeleteStudent(int id, CancellationToken cancellationToken);

        Task DeleteStudents(IEnumerable<int> ids, CancellationToken cancellationToken);
    }
}
