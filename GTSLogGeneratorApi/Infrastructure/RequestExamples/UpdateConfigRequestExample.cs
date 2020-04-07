using GTSLogGeneratorApi.Application.UpdateConfigRequest;
using Swashbuckle.AspNetCore.Filters;

namespace GTSLogGeneratorApi.Infrastructure.RequestExamples
{
    public class UpdateConfigRequestExample : IExamplesProvider<UpdateConfigRequest>
    {
        public UpdateConfigRequest GetExamples()
        {
            return new UpdateConfigRequest()
            {
                ConfigFilePath = "/home/krystian/Documents/Magisterka/GtsFolders/config.ini",
                ErrorLogsFolder = "/home/krystian/Documents/Magisterka/GtsFolders/errors_logs",
                LogsInputFolder = "/home/krystian/Documents/Magisterka/GtsFolders/logs_data",
                TimewindowSendCount = 3,
                InfluxdbHost = "localhost:9999",
                InfluxdbAuthToken = "auth_token",
                InfluxdbLogsMetricsBucket = "gts_logs_metrics",
                InfluxdbSystemMetricsBucket = "gts_system_metrics",
                InitialTimestampRoundBase = 1586277400,
                LoggerOutputFileLocation = "/home/krystian/Documents/Magisterka/GtsFolders/"
            };
        }
    }
}