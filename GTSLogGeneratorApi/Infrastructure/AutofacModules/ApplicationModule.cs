using Autofac;
using GTSLogGeneratorApi.Application.Jobs;
using GTSLogGeneratorApi.Application.Models;
using GTSLogGeneratorApi.Application.UpdateConfigRequest;
using GTSLogGeneratorApi.Application.UpdateLogsGenerationJobRequest;

namespace GTSLogGeneratorApi.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LogsGenerationJob>().As<ILogsGenerationJob>();
            builder.RegisterType<LogsGenerationJobParametersUpdater>().As<ILogsGenerationJobParametersUpdater>();
            builder.RegisterInstance(new ConfigParameters());
            builder.RegisterType<ConfigParametersUpdater>().As<IConfigParametersUpdater>();
        }
    }
}