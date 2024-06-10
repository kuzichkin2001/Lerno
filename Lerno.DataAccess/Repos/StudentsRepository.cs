using Lerno.DataAccess.DbContexts;
using Lerno.DataAccess.Interfaces;
using Lerno.Shared.Models;

namespace Lerno.DataAccess.Repos
{
    public class StudentsRepository : BaseRepository, IStudentsRepository
    {
        public StudentsRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public Task CreateStudentAsync(Student student, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task CreateStudentsAsync(IEnumerable<Student> students, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStudent(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStudents(IEnumerable<int> ids, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAllStudents(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserOfStudentAsync(string userName, string passwordHash, CancellationToken cancellationToken)
        {
            return new User()
            {
                Id = 1,
                FirstName = userName,
                LastName = passwordHash,
            };
        }

        public Task UpdateStudent(Student student, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
