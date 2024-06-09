using Lerno.Shared.Models;

namespace Lerno.DataAccess.Interfaces
{
    public interface ITeachersRepository
    {
        void CreateTeacher(Teacher teacher);

        void CreateTeachers(IEnumerable<Teacher> teachers);

        Teacher GetTeacher(string userName, string passwordHash);

        Teacher GetTeacher(string userNameFilter);

        IEnumerable<Teacher> GetAllTeachers();

        IEnumerable<Teacher> GetAllTeachersFiltered(int rating);

        void UpdateTeacher(Teacher teacher);

        void DeleteTeacher(int id);

        void DeleteTeachers(IEnumerable<int> ids);
    }
}
