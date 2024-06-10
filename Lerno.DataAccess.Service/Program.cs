using Lerno.DataAccess.DbContexts;
using Lerno.DataAccess.Interfaces;
using Lerno.DataAccess.Repos;
using Lerno.DataAccess.Service.Bus;
using Lerno.DataAccess.Service.Handlers;
using Lerno.Ext;
using Lerno.Shared.Enums;

namespace Lerno.DataAccess.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddDataAccessServiceConfiguration(configuration);
            builder.Services.AddSingleton<UnitOfWork>();
            builder.Services.AddTransient<IUsersRepository, UsersRepository>();
            builder.Services.AddTransient<IStudentsRepository, StudentsRepository>();
            builder.Services.AddTransient<ITeachersRepository, TeachersRepository>();
            builder.Services.AddTransient<IBusMessageHandlerFactory, BusMessageHandlerFactory>();

            builder.Services.AddHostedService<BusMessageListener>();

            var host = builder.Build();
            host.Run();
        }
    }
}