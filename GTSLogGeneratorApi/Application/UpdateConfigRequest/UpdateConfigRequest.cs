namespace GTSLogGeneratorApi.Application.UpdateConfigRequest
{
    public class UpdateConfigRequest
    {
        public bool LogInputFolder { get; set; }

        public int ErrorLogsFolder { get; set; }

        public int LoggerOutputFileLocation { get; set; }

        public int InitialTimestampRoundBase { get; set; }
        
        public int Timewindow { get; set; }

        public int TimewindowSendCount { get; set; }
        
        public string InfluxdbHost { get; set; }
        
        public string InfluxdbLogsMetricsBucket { get; set; }
        
        public string InfluxdbSystemMetricsBucket { get; set; }
    }
}