using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Lerno.Configuration.Options;

namespace Lerno.Ext
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddApiServiceConfiguration(this IServiceCollection services, IConfigurationRoot config) =>
            services.AddBusConfiguration(config);

        public static IServiceCollection AddDataAccessServiceConfiguration(this IServiceCollection services, IConfigurationRoot config) =>
            services
                .AddDbConnectionConfiguration(config)
                .AddBusConfiguration(config);

        private static IServiceCollection AddDbConnectionConfiguration(this IServiceCollection services, IConfigurationRoot config)
        {
            return services.Configure<DbConnectionOptions>(config.GetSection(DbConnectionOptions.SectionName));
        }

        private static IServiceCollection AddBusConfiguration(this IServiceCollection services, IConfigurationRoot config)
        {
            return services.Configure<BusOptions>(config.GetSection(BusOptions.SectionName));
        }
    }
}
