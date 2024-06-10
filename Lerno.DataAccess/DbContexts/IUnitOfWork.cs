using Lerno.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Lerno.DataAccess.DbContexts
{
    public interface IUnitOfWork : ICurrentDbContext
    {
        DbSet<Student> Students { get; }

        DbSet<User> Users { get; }

        DbSet<Teacher> Teachers { get; }
    }
}
