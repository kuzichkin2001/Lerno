using Lerno.DataAccess.Interfaces;
using Lerno.Shared.Models;

namespace Lerno.DataAccess.Repos
{
    public class StudentsRepository : IStudentsRepository
    {
        public void CreateStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public void CreateStudents(IEnumerable<Student> students)
        {
            throw new NotImplementedException();
        }

        public void DeleteStudent(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteStudents(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetAllStudentsFiltered(int rating)
        {
            throw new NotImplementedException();
        }

        public Student GetStudent(string userName, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Student GetStudent(string userNameFilter)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
