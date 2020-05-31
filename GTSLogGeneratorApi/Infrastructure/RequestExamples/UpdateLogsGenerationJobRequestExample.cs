using GTSLogGeneratorApi.Application.RunLogsGenerationJobRequest;
using GTSLogGeneratorApi.Application.UpdateConfigRequest;
using Swashbuckle.AspNetCore.Filters;

namespace GTSLogGeneratorApi.Infrastructure.RequestExamples
{
    public class UpdateLogsGenerationJobRequestExample : IExamplesProvider<RunLogsGenerationJobRequest>
    {
        public RunLogsGenerationJobRequest GetExamples()
        {
            return new RunLogsGenerationJobRequest()
            {
                Path = @"D:\Projects\Logs_data",
                Interval = 3,
                HostnamesCount = 2,
                ServerAddressesCount = 2,
                ProvidersCount = 2,
                UpstreamFqdnsCount = 2,
                HttpCodesCount = 2,
                CommunitiesCount = 2,
                LogsCount = 1000
            };
        }
    }
}