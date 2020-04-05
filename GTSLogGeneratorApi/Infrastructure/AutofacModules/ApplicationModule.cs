using Autofac;
using GTSLogGeneratorApi.Application.Jobs;

namespace GTSLogGeneratorApi.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LogsGenerationJob>().As<ILogsGenerationJob>();
        }
    }
}