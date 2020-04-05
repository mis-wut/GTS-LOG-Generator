using GTSLogGeneratorApi.Application.Models;
using GTSLogGeneratorApi.Infrastructure.Services;

namespace GTSLogGeneratorApi.Application.GetConfigRequest
{
    public class GetConfigResponseMapper : IMapper<ConfigParameters, GetConfigResponse>
    {
        public GetConfigResponse Map(ConfigParameters source)
        {
            if (source.ConfigFilePath == null)
            {
                return null;
            }
            
            return new GetConfigResponse()
            {
                ConfigFilePath = source.ConfigFilePath,
                LogsInputFolder = source.LogsInputFolder,
                ErrorLogsFolder = source.ErrorLogsFolder,
                LoggerOutputFileLocation = source.LoggerOutputFileLocation,
                InfluxdbHost = source.InfluxdbHost,
                InfluxdbLogsMetricsBucket = source.InfluxdbLogsMetricsBucket,
                InfluxdbSystemMetricsBucket = source.InfluxdbSystemMetricsBucket,
                InfxludbAuthToken = source.InfluxdbAuthToken,
                Timewindow = source.Timewindow,
                TimewindowSendCount = source.TimewindowSendCount,
                InitialTimestampRoundBase = source.InitialTimestampRoundBase
            };
        }
    }
}