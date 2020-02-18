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
        private readonly ILogsGenerationParametersResponseMapper _jobParametersMapper;

        public LogsGenerationController(IRecurringJobManager recurringJobManager,
            ILogsGenerationJob logsGenerationJob,
            ILogsGenerationParametersMapper parametersMapper,
            ILogsGenerationParametersResponseMapper jobParametersMapper)
        {
            _recurringJobManager = recurringJobManager;
            _logsGenerationJob = logsGenerationJob;
            _parametersMapper = parametersMapper;
            _jobParametersMapper = jobParametersMapper;
        }

        [HttpPut]
        [Route("UpdateLogGenerationJob")]
        public ActionResult UpdateLogGenerationJob(UpdateLogsGenerationJobRequest request)
        {
            var parameters = _parametersMapper.Map(request);
            LogsGenerationJob.Parameters = parameters;
            
            if (request.IsActive)
            {
                _recurringJobManager.AddOrUpdate(LogsGenerationJob.Id, () => _logsGenerationJob.Execute(parameters),
                    $"*/{request.Interval} * * * * *");
            }
            else
            {
                _recurringJobManager.RemoveIfExists(LogsGenerationJob.Id);
            }
            
            return Ok();
        }
        
        [HttpGet]
        [Route("GetLogGenerationJobParameters")]
        public ActionResult<LogsGenerationParametersResponse> GetLogGenerationJobParameters()
        {
            return _jobParametersMapper.Map(LogsGenerationJob.Parameters);
        }
    }
}
