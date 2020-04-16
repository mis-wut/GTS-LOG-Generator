using GTSLogGeneratorApi.Application.Jobs;
using GTSLogGeneratorApi.Application.Models;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GTSLogGeneratorApi.Application.UpdateLogsGenerationJobRequest
{
    public class UpdateLogsGenerationJobRequestHandler : RequestHandler<UpdateLogsGenerationJobRequest>
    {
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly ILogsGenerationJob _logsGenerationJob;
        private readonly ILogsGenerationJobParametersUpdater _parametersUpdater;

        public UpdateLogsGenerationJobRequestHandler(IRecurringJobManager recurringJobManager, 
            ILogsGenerationJob logsGenerationJob,
            ILogsGenerationJobParametersUpdater parametersUpdater)
        {
            _recurringJobManager = recurringJobManager;
            _logsGenerationJob = logsGenerationJob;
            _parametersUpdater = parametersUpdater;
        }

        protected override void Handle(UpdateLogsGenerationJobRequest request)
        {
            var parameters = _parametersUpdater.Update(request);
            
            if (request.IsActive)
            {
                UpdateGenerationJobInterval(parameters);
            }
            else
            {
                _recurringJobManager.RemoveIfExists(LogsGenerationJob.Id);
            }
        }

        private void UpdateGenerationJobInterval(LogsGenerationParameters parameters)
        {
            if (parameters.Interval < 60)
            {
                _recurringJobManager.AddOrUpdate(LogsGenerationJob.Id, () => _logsGenerationJob.Execute(parameters),
                    $"*/{parameters.Interval} * * * * *");
            }
            else if (parameters.Interval < 3600)
            {
                _recurringJobManager.AddOrUpdate(LogsGenerationJob.Id, () => _logsGenerationJob.Execute(parameters),
                    $"* */{parameters.Interval / 60} * * * *");
            }
            else
            {
                _recurringJobManager.AddOrUpdate(LogsGenerationJob.Id, () => _logsGenerationJob.Execute(parameters),
                    $"* * *{parameters.Interval / 3600} * * *");
            }
        }
    }
}