using Autofac;
using GTSLogGeneratorApi.Application.Jobs;
using GTSLogGeneratorApi.Application.Models;
using GTSLogGeneratorApi.Application.RunLogsGenerationJobRequest;
using GTSLogGeneratorApi.Application.UpdateConfigRequest;

namespace GTSLogGeneratorApi.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LogsGenerationJob>().As<ILogsGenerationJob>();
            builder.RegisterType<LogsGenerationJobParametersMapper>().As<ILogsGenerationJobParametersMapper>();
            builder.RegisterInstance(new ConfigParameters());
            builder.RegisterType<ConfigParametersUpdater>().As<IConfigParametersUpdater>();
        }
    }
}