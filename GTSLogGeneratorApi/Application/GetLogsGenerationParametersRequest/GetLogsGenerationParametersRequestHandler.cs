using System.Threading;
using System.Threading.Tasks;
using GTSLogGeneratorApi.Application.Jobs;
using GTSLogGeneratorApi.Controllers;
using GTSLogGeneratorApi.Infrastructure.Services;
using MediatR;

namespace GTSLogGeneratorApi.Application.GetLogsGenerationParametersRequest
{
    public class GetLogsGenerationParametersRequestHandler : IRequestHandler<GetLogsGenerationParametersRequest, GetLogsGenerationParametersResponse>
    {
        private readonly IMapper<LogsGenerationParameters, GetLogsGenerationParametersResponse> _parametersMapper;

        public GetLogsGenerationParametersRequestHandler(IMapper<LogsGenerationParameters, GetLogsGenerationParametersResponse> parametersMapper)
        {
            _parametersMapper = parametersMapper;
        }

        public Task<GetLogsGenerationParametersResponse> Handle(GetLogsGenerationParametersRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_parametersMapper.Map(LogsGenerationJob.Parameters));
        }
    }
}