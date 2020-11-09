namespace GTSLogGeneratorApi.Application.GetLogsGenerationLastParametersRequest
{
    public class GetLogsGenerationLastParametersResponse
    {
        public bool IsActive { get; set; }

        public int Interval { get; set; }

        public int LogsFilesCount { get; set; }

        public int LogsCount { get; set; }
        
        public int ContentClustersCount { get; set; }

        public int ServerAddressesCount { get; set; }
        
        public int HostnamesCount { get; set; }

        public int ProvidersCount { get; set; }

        public int UpstreamFqdnsCount { get; set; }

        public int HttpCodesCount { get; set; }

        public int CommunitiesCount { get; set; }

        public string Path { get; set; }
    }
}