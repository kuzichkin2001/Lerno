using Lerno.Shared.Models;

namespace Lerno.DataAccess.Interfaces
{
    public interface IStudentsRepository
    {
        void CreateStudent(Student student);

        void CreateStudents(IEnumerable<Student> students);

        Student GetStudent(string userName, string passwordHash);

        Student GetStudent(string userNameFilter);

        IEnumerable<Student> GetAllStudents();

        IEnumerable<Student> GetAllStudentsFiltered(int rating);

        void UpdateStudent(Student student);

        void DeleteStudent(int id);

        void DeleteStudents(IEnumerable<int> ids);
    }
}
