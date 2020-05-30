using GTSLogGeneratorApi.Application.Models;
using GTSLogGeneratorApi.Infrastructure.Services;

namespace GTSLogGeneratorApi.Application.GetLogsGenerationLastParametersRequest
{
    public class GetLogsGenerationLastParametersMapper : IMapper<LogsGenerationParameters, GetLogsGenerationLastParametersResponse>
    {
        public GetLogsGenerationLastParametersResponse Map(LogsGenerationParameters source)
        {
            if (source.Path == null)
            {
                return null;
            }
            
            return new GetLogsGenerationLastParametersResponse()
            {
                Interval = source.Interval,
                IsActive = source.IsActive,
                Path = source.Path,
                HostnamesCount = source.Hostnames.Count,
                ServerAddressesCount = source.ServerAddresses.Count,
                UpstreamFqdnsCount = source.UpstreamFqdns.Count,
                HttpCodesCount = source.HttpCodes.Count,
                CommunitiesCount = source.Communities.Count,
                LogsFilesCount = source.LogsFilesCount,
                LogsCount = source.LogsCount,
                ProvidersCount = source.Providers.Count
            };
        }
    }
}