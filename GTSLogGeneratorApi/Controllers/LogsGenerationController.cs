using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTSLogGeneratorApi.Jobs;
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

        public LogsGenerationController(IRecurringJobManager recurringJobManager,
            ILogsGenerationJob logsGenerationJob)
        {
            _recurringJobManager = recurringJobManager;
            _logsGenerationJob = logsGenerationJob;
        }

        [HttpPut]
        [Route("UpdateLogGenerationJob")]
        public ActionResult UpdateLogGenerationJob(LogsGenerationParameters parameters)
        {
            _recurringJobManager.AddOrUpdate(LogsGenerationJob.Id, () => _logsGenerationJob.Execute(parameters), $"*/{parameters.Interval} * * * * *");
            return Ok();
        }
    }
}
