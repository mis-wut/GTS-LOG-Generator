using GTSLogGeneratorApi.Application.Models;
using GTSLogGeneratorApi.Infrastructure.Services;

namespace GTSLogGeneratorApi.Application.GetLogsGenerationParametersRequest
{
    public class GetLogsGenerationParametersMapper : IMapper<LogsGenerationParameters, GetLogsGenerationParametersResponse>
    {
        public GetLogsGenerationParametersResponse Map(LogsGenerationParameters source)
        {
            if (source.Path == null)
            {
                return null;
            }
            
            return new GetLogsGenerationParametersResponse()
            {
                Interval = source.Interval,
                IsActive = source.IsActive,
                Path = source.Path,
                HostnamesCount = source.Hostnames.Count,
                ServerAddressesCount = source.ServerAddresses.Count,
                UpstreamFqdnsCount = source.UpstreamFqdns.Count,
                HttpCodesCount = source.HttpCodes.Count,
                LogsCount = source.LogsCount,
                ProvidersCount = source.Providers.Count
            };
        }
    }
}