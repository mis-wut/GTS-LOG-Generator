using GTSLogGeneratorApi.Application.Jobs;
using GTSLogGeneratorApi.Application.Models;
using GTSLogGeneratorApi.Infrastructure.Services;
using MediatR;

namespace GTSLogGeneratorApi.Application.GetLogsGenerationParametersRequest
{
    public class GetLogsGenerationParametersRequestHandler : RequestHandler<GetLogsGenerationParametersRequest, GetLogsGenerationParametersResponse>
    {
        private readonly IMapper<LogsGenerationParameters, GetLogsGenerationParametersResponse> _parametersMapper;

        public GetLogsGenerationParametersRequestHandler(IMapper<LogsGenerationParameters, GetLogsGenerationParametersResponse> parametersMapper)
        {
            _parametersMapper = parametersMapper;
        }

        protected override GetLogsGenerationParametersResponse Handle(GetLogsGenerationParametersRequest request)
        {
            return _parametersMapper.Map(LogsGenerationJob.Parameters);
        }
    }
}