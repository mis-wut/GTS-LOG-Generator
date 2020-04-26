using System.Threading;
using GTSLogGeneratorApi.Application.Jobs;
using GTSLogGeneratorApi.Application.Models;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GTSLogGeneratorApi.Application.UpdateLogsGenerationJobRequest
{
    public class UpdateLogsGenerationJobRequestHandler : RequestHandler<UpdateLogsGenerationJobRequest>
    {
        private readonly ILogsGenerationJob _logsGenerationJob;
        private readonly ILogsGenerationJobParametersUpdater _parametersUpdater;

        public UpdateLogsGenerationJobRequestHandler(ILogsGenerationJob logsGenerationJob,
            ILogsGenerationJobParametersUpdater parametersUpdater)
        {
            _logsGenerationJob = logsGenerationJob;
            _parametersUpdater = parametersUpdater;
        }

        protected override void Handle(UpdateLogsGenerationJobRequest request)
        {
            _parametersUpdater.Update(request);
        }
    }
}