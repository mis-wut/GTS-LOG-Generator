using GTSLogGeneratorApi.Application.Jobs;
using GTSLogGeneratorApi.Controllers;
using GTSLogGeneratorApi.Infrastructure.Services;

namespace GTSLogGeneratorApi.Application.GetLogsGenerationParametersRequest
{
    public class GetLogsGenerationParametersMapper : IMapper<LogsGenerationParameters, GetLogsGenerationParametersResponse>
    {
        public GetLogsGenerationParametersResponse Map(LogsGenerationParameters source)
        {
            if (source == null)
            {
                return null;
            }
            
            return new GetLogsGenerationParametersResponse()
            {
                Interval = source.Interval,
                IsActive = source.IsActive,
                Path = source.Path,
                ChannelsCount = source.Channels.Count,
                CitiesCount = source.Cities.Count,
                LogsCount = source.LogsCount,
                ProvidersCount = source.Providers.Count
            };
        }
    }
}