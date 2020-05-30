using System.Threading.Tasks;
using GTSLogGeneratorApi.Application.GetLogsGenerationLastParametersRequest;
using GTSLogGeneratorApi.Application.RunLogsGenerationJobRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GTSLogGeneratorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsGenerationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogsGenerationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("RunLogGenerationJob")]
        public async Task<ActionResult> RunLogGenerationJob(RunLogsGenerationJobRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        
        [HttpGet]
        [Route("GetLogGenerationJobLastParameters")]
        public async Task<ActionResult<GetLogsGenerationLastParametersResponse>> GetLogGenerationJobLastParameters()
        {
            return await _mediator.Send(new GetLogsGenerationLastParametersRequest());
        }
    }
}