using Lerno.Ext;
using Lerno.BusinessLogic.Services;
using Lerno.BusinessLogic.Interfaces;
using Lerno.SignalR.Shared.Hubs;
using Lerno.Shared.MappingProfiles;
using Lerno.BusinessLogic.Bus;
using AutoMapper;

namespace Lerno
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            var mapperConfiguration = new MapperConfiguration(mc =>
            {
                var defaultMappingProfile = new DefaultMappingProfile();
                mc.AddProfile(defaultMappingProfile);
            });

            // Add services to the container.

            builder.Services.AddApiServiceConfiguration(configuration);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IStudentsService, StudentsService>();
            builder.Services.AddSingleton<IBusMessageQueueService, BusMessageQueueService>();
            builder.Services.AddSingleton(mapperConfiguration.CreateMapper());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapHub<StreamDataHub>("/hubs/stream-data");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
