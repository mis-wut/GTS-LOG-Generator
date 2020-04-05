using MediatR;

namespace GTSLogGeneratorApi.Application.UpdateLogsGenerationJobRequest
{
    public class UpdateLogsGenerationJobRequest : IRequest
    {
        public bool IsActive { get; set; }

        public int Interval { get; set; }

        public int LogsCount { get; set; }

        public int CitiesCount { get; set; }
        
        public int ChannelsCount { get; set; }

        public int ProvidersCount { get; set; }
        
        public string Path { get; set; }
    }
}