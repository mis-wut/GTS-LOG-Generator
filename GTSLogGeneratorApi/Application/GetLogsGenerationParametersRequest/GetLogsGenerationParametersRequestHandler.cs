using GTSLogGeneratorApi.Application.Models;
using GTSLogGeneratorApi.Infrastructure.Services;
using MediatR;

namespace GTSLogGeneratorApi.Application.GetLogsGenerationParametersRequest
{
    public class GetLogsGenerationParametersRequestHandler : RequestHandler<GetLogsGenerationParametersRequest, GetLogsGenerationParametersResponse>
    {
        private readonly IMapper<LogsGenerationParameters, GetLogsGenerationParametersResponse> _parametersMapper;
        private readonly LogsGenerationParameters _parameters;

        public GetLogsGenerationParametersRequestHandler(IMapper<LogsGenerationParameters, GetLogsGenerationParametersResponse> parametersMapper, 
            LogsGenerationParameters parameters)
        {
            _parametersMapper = parametersMapper;
            _parameters = parameters;
        }

        protected override GetLogsGenerationParametersResponse Handle(GetLogsGenerationParametersRequest request)
        {
            return _parametersMapper.Map(_parameters);
        }
    }
}