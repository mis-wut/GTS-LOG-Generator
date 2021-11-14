using GTSLogGeneratorApi.Application.Jobs;
using GTSLogGeneratorApi.Application.Models;
using GTSLogGeneratorApi.Infrastructure.Services;
using MediatR;

namespace GTSLogGeneratorApi.Application.GetLogsGenerationLastParametersRequest
{
    public class GetLogsGenerationLastParametersRequestHandler : RequestHandler<GetLogsGenerationLastParametersRequest, GetLogsGenerationLastParametersResponse>
    {
        private readonly IMapper<LogsGenerationParameters, GetLogsGenerationLastParametersResponse> _parametersMapper;

        public GetLogsGenerationLastParametersRequestHandler(IMapper<LogsGenerationParameters, GetLogsGenerationLastParametersResponse> parametersMapper)
        {
            _parametersMapper = parametersMapper;
        }

        protected override GetLogsGenerationLastParametersResponse Handle(GetLogsGenerationLastParametersRequest request)
        {
            return _parametersMapper.Map(LogsGenerationJob.LastParameters);
        }
    }
}