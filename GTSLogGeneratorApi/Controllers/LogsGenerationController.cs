using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTSLogGeneratorApi.Jobs;
using GTSLogGeneratorApi.Mappers;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace GTSLogGeneratorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsGenerationController : ControllerBase
    {
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly ILogsGenerationJob _logsGenerationJob;
        private readonly ILogsGenerationParametersMapper _parametersMapper;

        public LogsGenerationController(IRecurringJobManager recurringJobManager,
            ILogsGenerationJob logsGenerationJob,
            ILogsGenerationParametersMapper parametersMapper)
        {
            _recurringJobManager = recurringJobManager;
            _logsGenerationJob = logsGenerationJob;
            _parametersMapper = parametersMapper;
        }

        [HttpPut]
        [Route("UpdateLogGenerationJob")]
        public ActionResult UpdateLogGenerationJob(UpdateLogsGenerationJobRequest request)
        {
            if (request.IsActive)
            {
                var parameters = _parametersMapper.Map(request);
                _recurringJobManager.AddOrUpdate(LogsGenerationJob.Id, () => _logsGenerationJob.Execute(parameters),
                    $"*/{request.Interval} * * * * *");
            }
            else
            {
                _recurringJobManager.RemoveIfExists(LogsGenerationJob.Id);
            }

            return Ok();
        }
    }
}
