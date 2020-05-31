using MediatR;

namespace GTSLogGeneratorApi.Application.RunLogsGenerationJobRequest
{
    public class RunLogsGenerationJobRequest : IRequest
    {
        public int Interval { get; set; }

        public int LogsFilesCount { get; set; }

        public int LogsCount { get; set; }

        public int ServerAddressesCount { get; set; }

        public int UpstreamFqdnsCount { get; set; }

        public int HostnamesCount { get; set; }

        public int ProvidersCount { get; set; }
     
        public int HttpCodesCount { get; set; }

        public int CommunitiesCount { get; set; }

        public string Path { get; set; }
    }
}