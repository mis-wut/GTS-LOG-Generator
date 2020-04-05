using MediatR;

namespace GTSLogGeneratorApi.Application.UpdateConfigRequest
{
    public class UpdateConfigRequestHandler : RequestHandler<UpdateConfigRequest>
    {
        private readonly IConfigParametersUpdater _configParametersUpdater;

        public UpdateConfigRequestHandler(IConfigParametersUpdater configParametersUpdater)
        {
            _configParametersUpdater = configParametersUpdater;
        }

        protected override void Handle(UpdateConfigRequest request)
        {
            _configParametersUpdater.Udpate(request);
            // TODO: Update pliku config.ini
        }
    }
}