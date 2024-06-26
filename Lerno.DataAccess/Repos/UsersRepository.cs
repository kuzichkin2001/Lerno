﻿using Lerno.DataAccess.DbContexts;
using Lerno.DataAccess.Interfaces;
using Lerno.Shared.Models;

namespace Lerno.DataAccess.Repos
{
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public UsersRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public void CreateUser(User user)
        {

            _unitOfWork.Users.Add(user);
            _unitOfWork.SaveChanges();
        }

        public void CreateUsers(IEnumerable<User> users)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteUsers(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.Users.Take(10);
        }

        public IEnumerable<User> GetAllUsersFiltered(int rating)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string userName, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string userNameFilter)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
