using MediatR;

namespace GTSLogGeneratorApi.Application.UpdateLogsGenerationJobRequest
{
    public class UpdateLogsGenerationJobRequest : IRequest
    {
        public bool IsActive { get; set; }

        public int Interval { get; set; }

        public int LogsCount { get; set; }

        public int ServerAddressesCount { get; set; }

        public int UpstreamFqdnsCount { get; set; }

        public int HostnamesCount { get; set; }

        public int ProvidersCount { get; set; }
     
        public int HttpCodesCount { get; set; }

        public string Path { get; set; }
    }
}