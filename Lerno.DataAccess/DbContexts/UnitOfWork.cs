using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Lerno.Shared.Models;
using Lerno.Configuration.Options;

namespace Lerno.DataAccess.DbContexts
{
    public class UnitOfWork : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }

        private readonly string _connectionString;

        public UnitOfWork(IOptions<DbConnectionOptions> connectionOptions)
        {
            _connectionString = connectionOptions.Value.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildUserModel(modelBuilder);
            BuildStudentModel(modelBuilder);
            BuildTeacherModel(modelBuilder);
        }

        private void BuildUserModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(eb =>
            {
                eb.ToTable("User", "dbo");
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id).HasColumnName("UserId").IsRequired().HasIdentityOptions(1, 1);
                eb.Property(x => x.LastName).IsRequired().HasDefaultValue(string.Empty);
                eb.Property(x => x.FirstName).IsRequired().HasDefaultValue(string.Empty);
                eb.Property(x => x.MiddleName);
            });
        }

        private void BuildStudentModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(eb =>
            {
                eb.ToTable("Student", "dbo");
                eb.HasKey(s => s.Id);
                eb.Property(s => s.Id).HasColumnName("StudentId");
                eb.HasOne(s => s.User).WithOne(u => u.Student);
            });
        }

        private void BuildTeacherModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
                .HasKey(x => x.Id);
        }
    }
}
