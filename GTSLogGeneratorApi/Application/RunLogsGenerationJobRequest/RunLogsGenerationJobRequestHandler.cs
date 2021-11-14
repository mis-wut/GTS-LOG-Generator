using GTSLogGeneratorApi.Application.Jobs;
using Hangfire;
using MediatR;

namespace GTSLogGeneratorApi.Application.RunLogsGenerationJobRequest
{
    public class RunLogsGenerationJobRequestHandler : RequestHandler<RunLogsGenerationJobRequest>
    {
        private readonly ILogsGenerationJobParametersMapper _parametersMapper;
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly ILogsGenerationJob _job;

        public RunLogsGenerationJobRequestHandler(ILogsGenerationJobParametersMapper parametersMapper,
            IBackgroundJobClient backgroundJobs,
            ILogsGenerationJob job)
        {
            _parametersMapper = parametersMapper;
            _backgroundJobs = backgroundJobs;
            _job = job;
        }

        protected override void Handle(RunLogsGenerationJobRequest request)
        {
            var parameters = _parametersMapper.Map(request);
            _backgroundJobs.Enqueue(() => _job.Execute(parameters));
        }
    }
}