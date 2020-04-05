using System.Threading;
using System.Threading.Tasks;
using GTSLogGeneratorApi.Application.Jobs;
using GTSLogGeneratorApi.Infrastructure.Services;
using Hangfire;
using MediatR;

namespace GTSLogGeneratorApi.Application.UpdateLogsGenerationJobRequest
{
    public class UpdateLogsGenerationJobRequestHandler : AsyncRequestHandler<UpdateLogsGenerationJobRequest>
    {
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly ILogsGenerationJob _logsGenerationJob;
        private readonly IMapper<UpdateLogsGenerationJobRequest, LogsGenerationParameters> _parametersMapper;

        public UpdateLogsGenerationJobRequestHandler(IRecurringJobManager recurringJobManager, 
            ILogsGenerationJob logsGenerationJob, 
            IMapper<UpdateLogsGenerationJobRequest, LogsGenerationParameters> parametersMapper)
        {
            _recurringJobManager = recurringJobManager;
            _logsGenerationJob = logsGenerationJob;
            _parametersMapper = parametersMapper;
        }

        protected override Task Handle(UpdateLogsGenerationJobRequest request, CancellationToken cancellationToken)
        {
            var parameters = _parametersMapper.Map(request);
            LogsGenerationJob.Parameters = parameters;
            
            if (parameters.IsActive)
            {
                UpdateGenerationJobInterval(parameters);
            }
            else
            {
                _recurringJobManager.RemoveIfExists(LogsGenerationJob.Id);
            }

            return Task.CompletedTask;
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