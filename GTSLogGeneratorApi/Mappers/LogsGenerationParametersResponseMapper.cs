using System.Collections.Generic;
using GTSLogGeneratorApi.Controllers;
using GTSLogGeneratorApi.Extensions;
using GTSLogGeneratorApi.Jobs;

namespace GTSLogGeneratorApi.Mappers
{
    public interface ILogsGenerationParametersResponseMapper
    {
        LogsGenerationParametersResponse Map(LogsGenerationParameters source);
    }

    public class LogsGenerationParametersResponseMapper : ILogsGenerationParametersResponseMapper
    {
        public LogsGenerationParametersResponse Map(LogsGenerationParameters source)
        {
            if (source == null)
            {
                return null;
            }
            
            return new LogsGenerationParametersResponse()
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