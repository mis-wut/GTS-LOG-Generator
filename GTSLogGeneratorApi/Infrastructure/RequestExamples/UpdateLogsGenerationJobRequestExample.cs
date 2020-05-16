using GTSLogGeneratorApi.Application.UpdateConfigRequest;
using GTSLogGeneratorApi.Application.UpdateLogsGenerationJobRequest;
using Swashbuckle.AspNetCore.Filters;

namespace GTSLogGeneratorApi.Infrastructure.RequestExamples
{
    public class UpdateLogsGenerationJobRequestExample : IExamplesProvider<UpdateLogsGenerationJobRequest>
    {
        public UpdateLogsGenerationJobRequest GetExamples()
        {
            return new UpdateLogsGenerationJobRequest()
            {
                Path = @"D:\Projects\Logs_data",
                Interval = 3,
                HostnamesCount = 2,
                ServerAddressesCount = 2,
                ProvidersCount = 2,
                UpstreamFqdnsCount = 2,
                HttpCodesCount = 2,
                CommunitiesCount = 2,
                IsActive = true,
                LogsCount = 1000
            };
        }
    }
}