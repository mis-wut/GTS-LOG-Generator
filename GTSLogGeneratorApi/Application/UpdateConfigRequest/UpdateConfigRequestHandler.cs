using IniParser;
using IniParser.Model;
using MediatR;

namespace GTSLogGeneratorApi.Application.UpdateConfigRequest
{
    public class UpdateConfigRequestHandler : RequestHandler<UpdateConfigRequest>
    {
        private readonly IConfigParametersUpdater _configParametersUpdater;

        public UpdateConfigRequestHandler(IConfigParametersUpdater configParametersUpdater)
        {
            _configParametersUpdater = configParametersUpdater;
        }

        protected override void Handle(UpdateConfigRequest request)
        {
            UpdateConfigFile(request);
            _configParametersUpdater.Udpate(request);
        }

        private void UpdateConfigFile(UpdateConfigRequest request)
        {
            var parser = new FileIniDataParser();
            var config = parser.ReadFile(request.ConfigFilePath);
            config["configuration"]["timewindow_length"] = request.Timewindow.ToString();
            config["configuration"]["timewindow_base"] = request.InitialTimestampRoundBase.ToString();
            config["configuration"]["logs_folder_path"] = request.LogsInputFolder;
            config["configuration"]["errors_folder_path"] = request.ErrorLogsFolder;
            config["configuration"]["logger_output_file_path"] = request.LoggerOutputFileLocation;
            config["configuration"]["influxdb_host"] = request.InfluxdbHost;
            config["configuration"]["influxdb_logs_bucket_name"] = request.InfluxdbLogsMetricsBucket;
            config["configuration"]["influxdb_system_bucket_name"] = request.InfluxdbSystemMetricsBucket;
            config["configuration"]["influxdb_auth_token"] = request.InfluxdbAuthToken;
            parser.WriteFile(request.ConfigFilePath ,config);
        }
    }
}