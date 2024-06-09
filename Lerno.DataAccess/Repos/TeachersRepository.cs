using Lerno.DataAccess.Interfaces;
using Lerno.Shared.Models;

namespace Lerno.DataAccess.Repos
{
    public class TeachersRepository : ITeachersRepository
    {
        public void CreateTeacher(Teacher teacher)
        {
            throw new NotImplementedException();
        }

        public void CreateTeachers(IEnumerable<Teacher> teachers)
        {
            throw new NotImplementedException();
        }

        public void DeleteTeacher(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteTeachers(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Teacher> GetAllTeachers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Teacher> GetAllTeachersFiltered(int rating)
        {
            throw new NotImplementedException();
        }

        public Teacher GetTeacher(string userName, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Teacher GetTeacher(string userNameFilter)
        {
            throw new NotImplementedException();
        }

        public void UpdateTeacher(Teacher teacher)
        {
            throw new NotImplementedException();
        }
    }
}
