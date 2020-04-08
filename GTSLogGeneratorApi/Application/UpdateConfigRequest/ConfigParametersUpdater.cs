using GTSLogGeneratorApi.Application.Models;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;

namespace GTSLogGeneratorApi.Application.UpdateConfigRequest
{
    public interface IConfigParametersUpdater
    {
        void Udpate(UpdateConfigRequest source);
    }
    
    public class ConfigParametersUpdater : IConfigParametersUpdater
    {
        private readonly ConfigParameters _parameters;

        public ConfigParametersUpdater(ConfigParameters parameters)
        {
            _parameters = parameters;
        }

        public void Udpate(UpdateConfigRequest source)
        {
            _parameters.ConfigFilePath = source.ConfigFilePath;
            _parameters.LogsInputFolder = source.LogsInputFolder;
            _parameters.ErrorLogsFolder = source.ErrorLogsFolder;
            _parameters.LoggerOutputFileLocation = source.LoggerOutputFileLocation;
            _parameters.InitialTimestampRoundBase = source.InitialTimestampRoundBase;
            _parameters.Timewindow = source.Timewindow;
            _parameters.InfluxdbHost = source.InfluxdbHost;
            _parameters.InfluxdbLogsMetricsBucket = source.InfluxdbLogsMetricsBucket;
            _parameters.InfluxdbSystemMetricsBucket = source.InfluxdbSystemMetricsBucket;
            _parameters.InfluxdbAuthToken = source.InfluxdbAuthToken;
        }
    }
}