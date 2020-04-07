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
                Path = "/home/krystian/Documents/Magisterka/GtsFolders/logs_data",
                Interval = 3,
                ChannelsCount = 2,
                CitiesCount = 2,
                IsActive = false,
                LogsCount = 1000,
                ProvidersCount = 2
            };
        }
    }
}