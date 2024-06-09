using Lerno.Shared.Models;

namespace Lerno.DataAccess.Interfaces
{
    public interface ITeachersRepository
    {
        void CreateTeacher(Teacher teacher);

        void CreateTeachers(IEnumerable<Teacher> teachers);

        Teacher GetTeacher(string userName, string passwordHash);

        IEnumerable<Teacher> GetAllTeachers();

        void UpdateTeacher(Teacher teacher);

        void DeleteTeacher(int id);

        void DeleteTeachers(IEnumerable<int> ids);
    }
}
