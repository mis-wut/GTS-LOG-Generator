using System.Threading.Tasks;
using GTSLogGeneratorApi.Application.GetConfigRequest;
using GTSLogGeneratorApi.Application.GetLogsGenerationParametersRequest;
using GTSLogGeneratorApi.Application.UpdateConfigRequest;
using GTSLogGeneratorApi.Application.UpdateLogsGenerationJobRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GTSLogGeneratorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsGenerationController : ControllerBase
    {
        private IMediator _mediator;

        public LogsGenerationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        [Route("UpdateLogGenerationJob")]
        public async Task<ActionResult> UpdateLogGenerationJob(UpdateLogsGenerationJobRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        
        [HttpGet]
        [Route("GetLogGenerationJobParameters")]
        public async Task<ActionResult<GetLogsGenerationParametersResponse>> GetLogGenerationJobParameters()
        {
            return await _mediator.Send(new GetLogsGenerationParametersRequest());
        }
    }
}