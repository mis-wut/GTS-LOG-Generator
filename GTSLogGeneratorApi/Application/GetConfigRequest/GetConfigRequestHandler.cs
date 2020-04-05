using GTSLogGeneratorApi.Application.Models;
using GTSLogGeneratorApi.Infrastructure.Services;
using MediatR;

namespace GTSLogGeneratorApi.Application.GetConfigRequest
{
    public class GetConfigRequestHandler : RequestHandler<GetConfigRequest, GetConfigResponse>
    {
        private readonly ConfigParameters _configParameters;
        private readonly IMapper<ConfigParameters, GetConfigResponse> _configParametersMapper;
        
        public GetConfigRequestHandler(ConfigParameters configParameters, 
            IMapper<ConfigParameters, GetConfigResponse> configParametersMapper)
        {
            _configParameters = configParameters;
            _configParametersMapper = configParametersMapper;
        }
        
        protected override GetConfigResponse Handle(GetConfigRequest request)
        {
            return  _configParametersMapper.Map(_configParameters);
        }
    }
}