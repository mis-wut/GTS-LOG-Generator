using System;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GTSLogGeneratorApi.Extensions.ServiceCollectionExtensions
{
    public static class Hangfire
    {
        public static IServiceCollection AddMemoryHangfire(this IServiceCollection services)
        {
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMemoryStorage());

            services.AddHangfireServer(x => x.SchedulePollingInterval = TimeSpan.FromSeconds(1));
            
            return services;
        }
    }
}